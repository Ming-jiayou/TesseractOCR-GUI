using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Prism.Mvvm;
using Prism.Commands;

namespace TesseractOCR_GUI.ViewModels
{
    public class CommandLineWrappingViewModel : BindableBase
    {
        public CommandLineWrappingViewModel()
        {
            SelectImageCommand = new DelegateCommand(ExecuteSelectImageCommand);
            OCRCommand = new DelegateCommand(ExecuteOCRCommand);
        }

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
            string command;
            switch(SelectedLanguage)
            {
                case "中文":
                    command = $"tesseract {SelectedFilePath} stdout -l chi_sim quiet";
                    break;
                case "英文":
                    command = $"tesseract {SelectedFilePath} stdout -l eng quiet";
                    break;
                default:
                    command = $"tesseract {SelectedFilePath} stdout -l chi_sim quiet";
                    break;
            }  

            // 创建一个新的 ProcessStartInfo 对象
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe", // 使用 cmd.exe 作为命令解释器
                Arguments = $"/c {command}", // 传递命令作为参数，/c 表示执行命令后退出
                RedirectStandardOutput = true, // 重定向标准输出
                RedirectStandardError = true, // 重定向标准错误
                UseShellExecute = false, // 不使用 Shell 执行
                CreateNoWindow = true, // 不创建新窗口
                StandardOutputEncoding = Encoding.GetEncoding("UTF-8"), // 设置标准输出的编码
                StandardErrorEncoding = Encoding.GetEncoding("UTF-8") // 设置标准错误的编码
            };

            // 创建一个新的 Process 对象
            Process process = new Process
            {
                StartInfo = processStartInfo
            };

            // 启动进程
            process.Start();

            // 读取输出
            OCRText = process.StandardOutput.ReadToEnd();

            // 读取错误（如果有）
            string error = process.StandardError.ReadToEnd();

            // 等待进程退出
            process.WaitForExit();
        }
    }
}
