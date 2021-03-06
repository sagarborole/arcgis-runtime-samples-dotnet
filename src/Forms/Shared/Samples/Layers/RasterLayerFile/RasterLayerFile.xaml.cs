// Copyright 2017 Esri.
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License.
// You may obtain a copy of the License at: http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific
// language governing permissions and limitations under the License.

using System;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Rasters;
using ArcGISRuntime.Samples.Managers;
using Xamarin.Forms;

namespace ArcGISRuntime.Samples.RasterLayerFile
{
    [ArcGISRuntime.Samples.Shared.Attributes.Sample(
        "Raster layer (file)",
        "Layers",
        "This sample demonstrates how to use a raster layer created from a local raster file.",
        "The raster file is downloaded by the sample viewer automatically. Note that due to a known bug, this sample may crash in emulators running Android 4.4 (API level 19). All other platform versions are unaffected.")]
	[ArcGISRuntime.Samples.Shared.Attributes.OfflineData("7c4c679ab06a4df19dc497f577f111bd")]
    public partial class RasterLayerFile : ContentPage
    {
        public RasterLayerFile()
        {
            InitializeComponent();

            // Call a function to set up the map
            Initialize();
        }

        private async void Initialize()
        {
            // Add an imagery basemap
            Map myMap = new Map(Basemap.CreateImagery());

            // Get the file name
            string filepath = GetRasterPath();

            // Load the raster file
            Raster myRasterFile = new Raster(filepath);

            // Create the layer
            RasterLayer myRasterLayer = new RasterLayer(myRasterFile);

            // Add the layer to the map
            myMap.OperationalLayers.Add(myRasterLayer);

            // Add map to the mapview
            MyMapView.Map = myMap;

            try
            {
                // Wait for the layer to load
                await myRasterLayer.LoadAsync();

                // Set the viewpoint
                await MyMapView.SetViewpointGeometryAsync(myRasterLayer.FullExtent);
            }
            catch (Exception e)
            {
                await ((Page)Parent).DisplayAlert("Error", e.ToString(), "OK");
            }
        }

        private string GetRasterPath()
        {
            return DataManager.GetDataFolder("7c4c679ab06a4df19dc497f577f111bd", "raster-file", "Shasta.tif");
        }
    }
}