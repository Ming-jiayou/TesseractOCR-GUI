using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TesseractOCR_GUI.Interop;

namespace TesseractOCR_GUI.ViewModels
{
    public class APIWrappingViewModel : BindableBase
    {
        public APIWrappingViewModel(TesseractWrapper tesseractWrapper)
        {
            SelectImageCommand = new DelegateCommand(ExecuteSelectImageCommand);
            OCRCommand = new DelegateCommand(ExecuteOCRCommand);
            _tesseractWrapper = tesseractWrapper;
        }

        private readonly TesseractWrapper _tesseractWrapper;

        private string? _selectedFilePath;
        public string? SelectedFilePath
        {
            get { return _selectedFilePath; }
            set { SetProperty(ref _selectedFilePath, value); }
        }

        private string? _selectedLanguage;
        public string? SelectedLanguage
        {
            get { return _selectedLanguage; }
            set { SetProperty(ref _selectedLanguage, value); }
        }

        private string? _ocrText;
        public string? OCRText
        {
            get { return _ocrText; }
            set { SetProperty(ref _ocrText, value); }
        }

        public List<string> LanguageOptions { get; set; } = new List<string> { "中文", "英文" };

        public ICommand SelectImageCommand { get; private set; }
        public ICommand OCRCommand { get; private set; }

        private void ExecuteSelectImageCommand()
        {
            // 创建 OpenFileDialog 实例
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*",
                Title = "选择图片文件"
            };

            // 显示对话框
            if (openFileDialog.ShowDialog() == true)
            {
                // 获取选中的文件路径
                SelectedFilePath = openFileDialog.FileName;
            }
        }

        private void ExecuteOCRCommand()
        {
            switch (SelectedLanguage)
            {
                case "中文":
                    if (SelectedFilePath == null)
                    {
                        return;
                    }
                    OCRText = _tesseractWrapper.GetChineseTextNative(SelectedFilePath);
                    break;
                case "英文":
                    if (SelectedFilePath == null)
                    {
                        return;
                    }
                    OCRText = _tesseractWrapper.GetEnglishTextNative(SelectedFilePath);
                    break;
                default:
                    if (SelectedFilePath == null)
                    {
                        return;
                    }
                    OCRText = _tesseractWrapper.GetChineseTextNative(SelectedFilePath);
                    break;
            }           
        }
    }
}
