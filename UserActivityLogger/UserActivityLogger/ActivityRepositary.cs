﻿using ActivityLogger;
using Core;
using EventPublisher;
using FileSystem;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;

namespace UserActivityLogger
{
    public class ActivityRepositary : IActivityRepositary
    {
        private IJarFileWriter _jarFileWriter;
        private IImageCommentEmbedder _imageCommentEmbedder;
        private readonly IJarFileFactory _jarFileFactory;
        IActivityReaderFactory activityReaderFactory;
        private string _dataFolder;
        IActivityReaderFactory _activityReaderFactory;
        public ActivityRepositary(IJarFileFactory jarFileFactory, 
            IImageCommentEmbedder imageCommentEmbedder, 
            IActivityReaderFactory activityReaderFactory)
        {
            _jarFileFactory = jarFileFactory;
            _imageCommentEmbedder = imageCommentEmbedder;
            _activityReaderFactory = activityReaderFactory;
            _dataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SysLogs");
        }

        public string DataFolder
        {
            get
            {
                return _dataFolder;
            }
        }

        public void Add(Activity activity)
        {
            CreateNewJarFileWriterIfRequired();
            var headers = new Dictionary<string, string>();
            var screenShotBytes = activity.ScreenShot.ToByteArray();
            JarFileItem item;

            using (var inputStream = new MemoryStream(screenShotBytes))
            {
                using (var outputStream = _imageCommentEmbedder.AddComment(new MemoryStream(screenShotBytes), activity.KeyPressedData))
                {
                     item = new JarFileItem(headers, outputStream.ToArray(), -1);
                }
            }
      
            try
            {
                _jarFileWriter.AddFile(item);
            }
            catch (JarFileReachedMaxLimitException)
            {
                //TO DO: move this event to appropiate class or remove
                EventContainer.PublishEvent(
                    Events.LogFileReachedMaxLimit.ToString(),
                    new EventArg(Guid.NewGuid(), _jarFileWriter.JarFilePath));

                CreateNewJarFileWriter();

                _jarFileWriter.AddFile(item);
            }
        }
        public IActivityReader GetReader(IEnumerable<string> files)
        {
            return  _activityReaderFactory.GetReader(files);
        }
        private void CreateNewJarFileWriter()
        {
            var ipUser = IPAddress.GetCurrentMachineIp() + RuntimeHelper.GetCurrentUserName();

            var logFilePath = Path.Combine(_dataFolder, ipUser.ReverseMe()) + "_" + Guid.NewGuid() + "."
                              + Constants.JarFileExtension;
            DisposeCurrentJarFile();

            _jarFileWriter = _jarFileFactory.GetJarFileWriter(logFilePath);
        }

        private void CreateNewJarFileWriterIfRequired()
        {
            if (_jarFileWriter == null)
            {
                CreateNewJarFileWriter();
            }
        }

        private void DisposeCurrentJarFile()
        {
            if (_jarFileWriter != null)
            {
                _jarFileWriter.Dispose();
            }
        }
    }
}
