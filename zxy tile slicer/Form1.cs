using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace zxy_tile_slicer
{
    public partial class Form1 : Form
    {
        public const int c_TileSize = 256;
        private zoomLevels m_zoomLevels;
        private Image m_imgSource;
        private string m_destination;
        private string m_saveExtenstion;
        private ImageFormat m_saveFormat;
        private zoomLevel m_selectedLevel;
        private string m_strProcessingTemplate = "Level {0} of {1}" + Environment.NewLine + "{2}";
        private string m_strStepResize = "Resizing image to zoom level size.";
        private string m_strStepTiles = "Creating zoom level tiles.";

        public enum projection {
            Mercator = 0,
            Equirectangular = 1
        }

        public Form1()
        {
            InitializeComponent();
			try
			{
				tbxHelp.Text = "This tool was built to generate tiles for map zoom levels with the intent to use leaflet.js (http://leafletjs.com/)." + Environment.NewLine +
								Environment.NewLine +
								"First select the projection type you want to use. Go to https://en.wikipedia.org/wiki/List_of_map_projections for their definitions." + Environment.NewLine +
								"Most mapping tools normally use Mercator as it displays square and distorts more vertically the closer to the poles you get but never show the actual poles." + Environment.NewLine +
								"Equirectangular is twice as wide as it is tall and latitudes are equal distance from each other causing all latitude and longitude intersections to form squares." + Environment.NewLine +
								Environment.NewLine +
								"Once you pick the projection, open your file you want to create the tiles for. This will load the tile and pad the height or width to match the proper starting dimensions for the projection type selected and show you a preview once loaded of the full image. It will also set the zoom selection drop down to the range of options up to the maximum zoom. If your image size falls bewteen the pixel sizes of two zoom levels it will allow you to zoom up to the next zoom level if you want. Please note this will cause your highest zoom to be slightly pixelated or blurry as it is expanding the image beyond its native resolution." + Environment.NewLine +
								Environment.NewLine +
								"Choose the destination folder, this will create sub folders for each zoom level based on the zxy zoom structure." + Environment.NewLine +
								Environment.NewLine +
								"Select the zoom level you would like to be able to zoom into." + Environment.NewLine +
								Environment.NewLine +
								"Select Create tiles and your image will be processed and generate the zoomable tiles for your image. Once complete a popup will inform you it is done. Zoom tiles will be saved in the same format as the loaded source image, for example if a jpg is loaded it will create jpg tiles." + Environment.NewLine +
								Environment.NewLine +
								"If an error occurs it will appear in the lower left." + Environment.NewLine +
								Environment.NewLine +
								"Notes:" + Environment.NewLine +
								"The largest image that has been tested at this time successfully was 25000 pixels wide and 12500 tall in the Equirectangular projection format and created zoom tiles up to level 8. " + Environment.NewLine +
								"There may be some limitations based on the amount of ram available in your computer. This application runs in 64 bit mode." + Environment.NewLine +
								"If a zoom level is too large to resize and tile from based on available resources the tool will create smaller sections and generate the tiles from each sub section. This may cause some non-ideal appearance at the tile edges when you zoom to that level or beyond.";
			}
			catch (Exception ex) {
                string msg = ex.StackTrace;
            }
			grpHelp.Visible = false;
		}

        /// <summary>
        /// on load of file
        ///     Determine file type to define output format.
        ///     Calculate maximum zoom level, if between two levels allow to go up to next level
        /// on save
        ///     Display current zoom level working on of maximum level
        ///     Display % of tiles complete for level
        ///     Create resized working image copy for current zoom level
        ///     If image not square, create square with white/transparent(depending on file type) with current image centered in new size.
        ///     Crop and save each tile
        ///     Place tiles in folder for zoom level(base zero)
        ///     create sub folder for each column(x) with the column number(base zero).  -->
        ///     add a tile for each tile in column(y) with a name for the location(base zero). \|/
        ///     Create zoom levels up to the selected zoom in the combo box.
        ///     Save files in the same image format as the loaded file.
        ///
        ///http://www.thunderforest.com/tutorials/tile-format/
        ///https://msdn.microsoft.com/en-us/library/bb259689.aspx
        ///http://leafletjs.com/reference.html#tilelayer
        ///http://stackoverflow.com/questions/1922040/resize-an-image-c-sharp
        ///http://stackoverflow.com/questions/734930/how-to-crop-an-image-using-c
        ///
        /// To Do:
        /// - multi thread processing
        /// - better progress updates per zoomlevel and when processing sub sections for zoom levels able to be processed all at once.
        /// - selectable output file types including 32 bit png for transaprent overlay tiles
        /// - selectable output format to support zyx (current), Google maps, Bing maps, other tile formats
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // reset components
            tbxSourceFile.Text = "";
            lblBaseHeight.Text = "";
            lblBaseWidth.Text = "";
            lblBaseZoom.Text = "0";
            tbxDestination.Text = "";
            cboZoomSelect.Controls.Clear();
            btnDestination.Enabled = false;
            btnSave.Enabled = false;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            int intWidth;
            int intHeight;
            int intMaxZoom = 0;
            tbxError.Text = "";

            // Create OpenFileDialog 
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                // Set filter for file extension and default file extension 
                openFileDialog1.DefaultExt = ".png";
                openFileDialog1.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg|JPG Files (*.jpg)|*.jpg";

                try
                {
                    switch (openFileDialog1.ShowDialog())
                    {
                        case DialogResult.OK:
                            tbxProcessing.Text = "Image Loading";

                            //valid file, preview and set options
                            m_imgSource = new Bitmap(openFileDialog1.FileName);
                            intWidth = m_imgSource.Width;
                            intHeight = m_imgSource.Height;

                            // map projections: https://en.wikipedia.org/wiki/List_of_map_projections
                            if (rbtnMerator.Checked) {
                                m_zoomLevels = new zoomLevels(projection.Mercator);
                                // make sure source is square
                                m_imgSource = squareImage(ref m_imgSource, Math.Max(intWidth, intHeight));
                            }
                            else if (rbtnEquirectangular.Checked) {
                                // make sure source is 2x1 ratio
                                m_zoomLevels = new zoomLevels(projection.Equirectangular);
                                m_imgSource = equirectangularImage(ref m_imgSource, Math.Max(intWidth, intHeight), Math.Max(intWidth, intHeight)/2);
                            }

                            tbxSourceFile.Text = openFileDialog1.FileName;
                            lblBaseWidth.Text = intWidth.ToString();
                            lblBaseHeight.Text = intHeight.ToString();
                            pictureBox1.Image = m_imgSource;
                            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                            pictureBox1.MaximumSize = new Size(400, 400);

                            cboZoomSelect.Items.Clear();

                            switch (m_saveExtenstion)
                            {
                                case ".jpg":
                                    m_saveFormat = ImageFormat.Jpeg;
                                    break;
                                case ".png":
                                    m_saveFormat = ImageFormat.Png;
                                    break;
                                default:
                                    m_saveFormat = ImageFormat.Jpeg;
                                    break;
                            }

                            m_saveExtenstion = Path.GetExtension(tbxSourceFile.Text.ToLower());

                            foreach (zoomLevel level in m_zoomLevels.level)
                            {
                                if (level.pixelsX <= Math.Max(intWidth, intHeight) || (
                                        level.pixelsX > Math.Max(intWidth, intHeight) &&
                                        m_zoomLevels.level[intMaxZoom].pixelsX < Math.Max(intWidth, intHeight)
                                        )
                                    )
                                {
                                    //set zoom choices and determine max zoom.
                                    cboItem item = new cboItem();
                                    item.Text = level.zyxLevel.ToString() + " - " + level.pixelsX.ToString() + "px";
                                    item.Value = level.zyxLevel;
                                    cboZoomSelect.Items.Add(item);
                                    intMaxZoom = level.zyxLevel;
                                }
                                else
                                {
                                    break;
                                }
                            }

                            cboZoomSelect.SelectedIndex = intMaxZoom;
                            lblBaseZoom.Text = intMaxZoom.ToString();
                            btnDestination.Enabled = true;

                            tbxProcessing.Text = "";
                            break;

                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    tbxError.Text = "open error" + Environment.NewLine + "message:" + Environment.NewLine + ex.Message + Environment.NewLine + "stackTrace:" + Environment.NewLine + ex.StackTrace;
                }
            }
        }

        private void btnDestination_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlgFldr = new FolderBrowserDialog())
            {
                switch (dlgFldr.ShowDialog())
                {
                    case DialogResult.OK:
                        m_destination = dlgFldr.SelectedPath;
                        tbxDestination.Text = m_destination;
                        btnSave.Enabled = true;
                        break;
                    default:
                        btnSave.Enabled = false;
                        break;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //determine output and call build function
            if (m_imgSource != null && m_destination != null)
            {
                // get selected max zoom
                m_selectedLevel = m_zoomLevels.level[cboZoomSelect.SelectedIndex];

                if (generateZXYTiles(m_selectedLevel.zyxLevel))
                {
                    DialogResult dlgR = MessageBox.Show("Tiles created at " + m_destination, "Complete", MessageBoxButtons.OK);
                }
            }
        }

        private bool generateZXYTiles(int intMaxZoom) {
            Image imgCurrent;
            zoomLevel currLevel, maxLevel = null;
            bool blnReturn = true;
            int intCurrentZ = 0;
            int intMaxLevelRow = 0;
            int intMaxLevelColumn = 0;
            int intMaxGoodZoomLevel = 0;
            int intGroupCountX = 0;
            int intCurrXGroup = 0;
            int intCurrYGroup = 0;
            int tileXoffset, tileYoffset, tileXpixels, tileYpixels;
            bool successfulResize = false;
            bool foundMaxResize = false;

            tbxError.Text = "";

            try
            {
                // find the maximum successful reize level possible
                if (!foundMaxResize)
                {
                    // find the maximum size that the image can be successfully resized to.
                    while (!successfulResize)
                    {
                        successfulResize = false;
                        for (int i = intMaxZoom; i > 0; i--)
                        {
                            try
                            {
                                // get zoom settings for test size
                                maxLevel = m_zoomLevels.level.Find(x => x.zyxLevel.Equals(i));
                                
                                // resize
                                imgCurrent = resize(m_imgSource, maxLevel.pixelsX, maxLevel.pixelsY);

                                // successful resize, set result value
                                imgCurrent.Dispose();
                                intMaxGoodZoomLevel = i;
                                successfulResize = true;
                                break;
                            }
                            catch
                            {
                                // resize failed, try next size smaller
                                successfulResize = false;
                            }
                        }
                        // quit if no zoom level works;
                        if (intMaxGoodZoomLevel == 0)
                        {   
                            break;
                        }
                    }
                }

                for (intCurrentZ = 0; intCurrentZ <= intMaxZoom; intCurrentZ++)
                {
                    // resize image to zoom level
                    currLevel = m_zoomLevels.level.Find(x => x.zyxLevel.Equals(intCurrentZ));
                    intMaxLevelColumn = currLevel.tilesX;
                    intMaxLevelRow = currLevel.tilesY;
                    
                    if (maxLevel.zyxLevel >= currLevel.zyxLevel)
                    {
                        intCurrXGroup = 0;
                        intCurrYGroup = 0;
                        imgCurrent = resize(m_imgSource, currLevel.pixelsX, currLevel.pixelsY);
                        intGroupCountX = 0;
                        createTiles(ref imgCurrent, intCurrXGroup, intCurrYGroup, intCurrentZ, currLevel);
                        imgCurrent.Dispose();
                    }
                    else
                    {
                        // current level larger than max capable level, process in sections
                        intGroupCountX = (currLevel.tilesX / maxLevel.tilesX);
                        for (intCurrXGroup = 0; intCurrXGroup < intGroupCountX; intCurrXGroup++)
                        {
                            for (intCurrYGroup = 0; intCurrYGroup < intGroupCountX; intCurrYGroup++)
                            {
                                tileXoffset = intCurrXGroup;
                                tileYoffset = intCurrYGroup;
                                tileXpixels = (m_imgSource.Width / intGroupCountX);
                                tileYpixels = (m_imgSource.Height / intGroupCountX);

                                imgCurrent = cropImage(m_imgSource, tileXoffset, tileYoffset, tileXpixels, tileYpixels);
                                imgCurrent = resize(imgCurrent, maxLevel.pixelsX, maxLevel.pixelsY);
                                createTiles(ref imgCurrent, intCurrXGroup, intCurrYGroup, intCurrentZ, maxLevel);
                                imgCurrent.Dispose();
                            }
                        }
                    }
                }

                tbxProcessing.Text = "";
                //progressProcessing.Visible = false;
                //progressProcessing.Value = 0;

            }
            catch (Exception ex)
            {
				tbxError.Text = "Tile creation error" + Environment.NewLine + "message:" + Environment.NewLine + ex.Message + Environment.NewLine + "stackTrace:" + Environment.NewLine + ex.StackTrace;
				blnReturn = false;
            }

            return blnReturn;
        }

        private bool createTiles(ref Image imgCurrent, int intCurrXGroup, int intCurrYGroup, int intCurrentZ, zoomLevel currLevel) {
            int intCurrentX = 0;
            int intCurrentY = 0;
            int intMaxLevelRow = 0;
            int intMaxLevelColumn = 0;
            int intCurrentTileCount = 0;
            string zFolder = "";
            string xFolder = "";
            string yFile = "";
            string saveFilePath;
            Boolean blnReturn = true;

            // update labels
            tbxProcessing.Text = string.Format(m_strProcessingTemplate, intCurrentZ, m_selectedLevel.zyxLevel, m_strStepResize);
            progressProcessing.Visible = true;
            progressProcessing.Value = 0;
            progressProcessing.Minimum = 0;
            progressProcessing.Maximum = currLevel.tilesX * currLevel.tilesY;

            // create zoom (z) folder
            zFolder = intCurrentZ.ToString();
            saveFilePath = m_destination + "\\" + zFolder + "\\";
            createFolder(saveFilePath);

            intMaxLevelColumn = currLevel.tilesX;
            intMaxLevelRow = currLevel.tilesY;

            for (intCurrentX = 0; intCurrentX < intMaxLevelColumn; intCurrentX++)
            {
                // create column (x) folder
                xFolder = ((intCurrXGroup * currLevel.tilesX) + intCurrentX).ToString();
                saveFilePath = m_destination + "\\" + zFolder + "\\" + xFolder + "\\";
                createFolder(saveFilePath);

                for (intCurrentY = 0; intCurrentY < intMaxLevelRow; intCurrentY++)
                {
                    tbxProcessing.Text = string.Format(m_strProcessingTemplate, intCurrentZ, m_selectedLevel.zyxLevel, m_strStepTiles);

                    yFile = ((intCurrYGroup * currLevel.tilesY) + intCurrentY).ToString();
                    using (Image imgTile = cropImage(imgCurrent, intCurrentX, intCurrentY, c_TileSize, c_TileSize))
                    {
                        // save file
                        saveFilePath = m_destination + "\\" + zFolder + "\\" + xFolder + "\\" + yFile + m_saveExtenstion;
                        imgTile.Save(saveFilePath, m_saveFormat);
                    }

                    intCurrentTileCount++;
                    progressProcessing.Value = intCurrentTileCount;
                }
            }
            return blnReturn;
        }

        private void createFolder(string targetFolder)
        {
            FileInfo file = new FileInfo(targetFolder);
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
        }

        #region "Image processing"

        /// <summary>
        /// Returns resized, squared off image for the zoom level to process.
        /// based on answer in http://stackoverflow.com/questions/2823200/make-square-image
        /// </summary>
        /// <param name="originalImage"></param>
        /// <param name="targetSize"></param>
        /// <returns></returns>
        private Image squareImage(ref Image originalImage, int targetSize)
        {
            int largestDimension = targetSize;
            Size squareSize = new Size(largestDimension, largestDimension);
            Bitmap returnImage = new Bitmap(squareSize.Width, squareSize.Height, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(returnImage))
            {
                g.FillRectangle(Brushes.White, 0, 0, squareSize.Width, squareSize.Height);
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;

                g.DrawImage(originalImage, (squareSize.Width / 2) - (originalImage.Width / 2), (squareSize.Height / 2) - (originalImage.Height / 2), originalImage.Width, originalImage.Height);
            }

            return returnImage;
        }

        /// <summary>
        /// Returns resized, squared off image for the zoom level to process.
        /// based on answer in http://stackoverflow.com/questions/2823200/make-square-image
        /// </summary>
        /// <param name="originalImage"></param>
        /// <param name="targetSizeX"></param>
        /// <param name="targetSizeY"></param>
        /// <returns></returns>
        private Image equirectangularImage(ref Image originalImage, int targetSizeX, int targetSizeY)
        {
            Size squareSize = new Size(targetSizeX, targetSizeY);
            Bitmap returnImage = new Bitmap(squareSize.Width, squareSize.Height, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(returnImage))
            {
                g.FillRectangle(Brushes.White, 0, 0, squareSize.Width, squareSize.Height);
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;

                g.DrawImage(originalImage, (squareSize.Width / 2) - (originalImage.Width / 2), (squareSize.Height / 2) - (originalImage.Height / 2), originalImage.Width, originalImage.Height);
            }

            return returnImage;
        }

        /// <summary>
        /// Returns resized, squared off image for the zoom level to process.
        /// based on answer in http://stackoverflow.com/questions/1922040/resize-an-image-c-sharp
        /// </summary>
        /// <param name="originalImage"></param>
        /// <param name="targetXSize"></param>
        /// <returns></returns>
        private Image resize(Image originalImage, int targetXSize, int targetYSize)
        {
            Bitmap outputImage = new Bitmap(targetXSize, targetYSize);

            outputImage.SetResolution(originalImage.HorizontalResolution, originalImage.VerticalResolution);

            using (Graphics g = Graphics.FromImage(outputImage))
            {
                Rectangle destRect = new Rectangle(0, 0, targetXSize, targetYSize);
                g.CompositingMode = CompositingMode.SourceCopy;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (ImageAttributes wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    g.DrawImage(originalImage, destRect, 0, 0, originalImage.Width, originalImage.Height, GraphicsUnit.Pixel, wrapMode);
                }
                
            }
            //originalImage.Dispose();
            return outputImage;
        }

        /// <summary>
        /// Referenced http://stackoverflow.com/questions/734930/how-to-crop-an-image-using-c
        /// </summary>
        /// <param name="tileXoffset">Column number for tile to generate.</param>
        /// <param name="tileYoffset">Row number for tile to generate.</param>
        /// <returns></returns>
        private Image cropImage(Image origionalImage, int tileXoffset, int tileYoffset, int tileXpixels, int tileYpixels)
        {
            Bitmap outputTile = new Bitmap(tileXpixels, tileYpixels);
            Rectangle cropRect = new Rectangle(tileXoffset * tileXpixels, tileYoffset * tileYpixels, outputTile.Width, outputTile.Height);
            using (Graphics g = Graphics.FromImage(outputTile))
            {
                g.DrawImage(origionalImage,
                            new Rectangle(0, 0, tileXpixels, tileYpixels),
                            cropRect,
                            GraphicsUnit.Pixel);
            }
            return outputTile;
        }
		#endregion

		private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			grpHelp.Visible = true;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			grpHelp.Visible = false;
		}

    }

    #region "custom object classes"
    public partial class zoomLevel
    {
        public int zyxLevel;
        public int bingLevel;
        public int googleLevel;
        public int pixelsX;
        public int pixelsY;
        public int tilesX;
        public int tilesY;
        public double ResolutionPerPixelMi;
        public double ResolutionPerPixelFt;
        public double ResolutionPerPixelKM;
        public double ResolutionPerPixelM;
    }

    public partial class zoomLevels
    {
        public readonly List<zoomLevel> level;

        public zoomLevels(Form1.projection projection)
        {
            level = new List<zoomLevel>();

            for (int i = 0; i < 24; i++)
            {
                zoomLevel currentLevel = new zoomLevel();
                currentLevel.zyxLevel = i;
                if (i - 1 >= 0)
                {
                    currentLevel.bingLevel = i - 1;
                };
                switch (projection) {
                    case Form1.projection.Mercator:
                        currentLevel.pixelsX = Form1.c_TileSize * (int)Math.Pow(2, i);
                        currentLevel.tilesX = currentLevel.pixelsX / Form1.c_TileSize;
                        break;
                    case Form1.projection.Equirectangular:
                        currentLevel.pixelsX = (Form1.c_TileSize * (int)Math.Pow(2, i))*2;
                        currentLevel.tilesX = (currentLevel.pixelsX / Form1.c_TileSize);
                        break;
                    default:
                        break;
                }
                currentLevel.pixelsY = Form1.c_TileSize * (int)Math.Pow(2, i);
                currentLevel.tilesY = currentLevel.pixelsY / Form1.c_TileSize;
                currentLevel.ResolutionPerPixelMi = 24901.0 / currentLevel.pixelsX;
                currentLevel.ResolutionPerPixelFt = 131477280.0 / currentLevel.pixelsX;
                currentLevel.ResolutionPerPixelKM = 40075.0 / currentLevel.pixelsX;
                currentLevel.ResolutionPerPixelM = 40075000.0 / currentLevel.pixelsX;
                //if (Math.Max(currentLevel.pixelsX,currentLevel.pixelsY) <= Math.Pow(2,15)
                //{
                level.Add(currentLevel);
                //}
                
            };
        }
    }

    public class cboItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
    #endregion
}
