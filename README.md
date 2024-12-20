[简体中文](./README.zh.md) | English

# TesseractOCR-GUI

## A Simple User Interface for Tesseract OCR Based on WPF/C#.

## Screen Captures

![image-20241207112935860](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241207112935860.png)

![image-20241207113004039](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241207113004039.png)

## General user usage✨

The previous article [Using Tesseract for Image Text Recognition](https://mp.weixin.qq.com/s/C2o0-RtubtQb4pzys2wx6w) introduced how to install TesseractOCR and use it via the command line. However, using the command line in daily tasks can be inconvenient. Therefore, today we will introduce how to create a simple and user-friendly interface for TesseractOCR using WPF/C#.

Following the previous tutorial, after installing TesseractOCR locally, download from the GitHub Releases page.

GitHub address: https://github.com/Ming-jiayou/TesseractOCR-GUI

![image-20241207134914277](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241207134914277.png)

It is recommended to choose the compressed package with dependencies, as it is relatively smaller in size:

![image-20241207135004215](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241207135004215.png)

Unzip as shown below:

![image-20241207135159013](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241207135159013.png)

Double-click to open and use it. If it shows that you do not have the framework installed, click the link, download and install the framework, then you can open and use it.

Chinese recognition:

![image-20241207135447692](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241207135447692.png)

English recognition:

![image-20241207135519142](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241207135519142.png)

It is very simple and convenient to use.

## WPF/C# programmers usage✨

After a brief investigation, it was found that building a TesseractOCR-GUI can primarily be achieved in two ways. One way is to encapsulate the use of the command line, while the other is to encapsulate the TesseractOCR C++ API.

Encapsulating the use of the command line is simpler and currently meets my usage requirements, so only this method has been implemented for now. Pytesseract also seems to use this approach. The second method, which involves calling the Tesseract C++ API, might only be explored if the first method of encapsulating command line usage fails to meet the requirements.

Project structure:

![image-20241207140458038](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241207140458038.png)

Development tool: Visual Studio 2022

.NET version: .NET 8

Packages used: Prism + WPF UI

Core code:

```csharp
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
```

## Finally

This project can help people use TesseractOCR-GUI more easily and conveniently. For novice WPF/C# programmers, it can also serve as a simple practice project.

If it is helpful to you, giving it a star⭐ would be the greatest support!!

If you have any questions, feel free to contact me through my WeChat official account:

![qrcode_for_gh_eb0908859e11_344](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/qrcode_for_gh_eb0908859e11_344.jpg)
