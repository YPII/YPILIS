using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace YellowstonePathology.Business
{
	public class FtpTransfer
	{
		private string m_UserName;
		private string m_Password;
		private string m_RemotePath;
		private string m_BatchFileName;
		private string m_FileToExecute;

		public FtpTransfer(string userName, string password, string remotePath)
		{
			this.m_UserName = userName;
			this.m_Password = password;
			this.m_RemotePath = remotePath;
			this.m_BatchFileName = @"C:\Program Files\Yellowstone Pathology Institute\PSATransfer.txt";
			this.m_FileToExecute = @"C:\Program Files\Yellowstone Pathology Institute\psftp.exe";
		}

		public YellowstonePathology.Business.Rules.MethodResult Upload(string localFileFullName)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = new Rules.MethodResult();
			methodResult.Success = true;

			string fileName = Path.GetFileName(localFileFullName);
			string uploadFileName = Path.Combine(this.m_RemotePath, fileName);
			uploadFileName = uploadFileName.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

			FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uploadFileName);
			request.Method = WebRequestMethods.Ftp.UploadFile;
			request.KeepAlive = false;
			request.UseBinary = true;
			request.UsePassive = false;
			request.Credentials = new NetworkCredential(this.m_UserName, this.m_Password);


			byte[] fileContents = File.ReadAllBytes(localFileFullName);

			Stream requestStream = request.GetRequestStream();
			requestStream.Write(fileContents, 0, fileContents.Length);
			requestStream.Close();

			try
			{
				FtpWebResponse response = (FtpWebResponse)request.GetResponse();
				if (response.StatusDescription.Substring(0, 3) != "226") //Indicate Transfer Complete
				{
					methodResult.Message = "FTP Upload failed for " + localFileFullName + ".";
					methodResult.Success = false;
				}

				response.Close();
			}
			catch
			{
				methodResult.Message = "FTP Upload failed for " + localFileFullName + ".";
				methodResult.Success = false;
			}
			return methodResult;
		}

		public YellowstonePathology.Business.Rules.MethodResult CreateRemoteFolder(string folderName)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = new Rules.MethodResult();
			methodResult.Success = true;

			string uploadFolderName = Path.Combine(this.m_RemotePath, folderName);
			uploadFolderName = uploadFolderName.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

			FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uploadFolderName);
			request.Method = WebRequestMethods.Ftp.MakeDirectory;
			request.KeepAlive = false;
			request.UsePassive = false;
			request.Credentials = new NetworkCredential(this.m_UserName, this.m_Password);

			FtpWebResponse response = null;
			try
			{
				response = (FtpWebResponse)request.GetResponse();
				if (response.StatusDescription.Substring(0, 3) != "257") //Indicate Creation Success
				{
					methodResult.Message = "FTP Directory Creation failed for " + folderName + ".";
					methodResult.Success = false;
				}

				response.Close();
			}
			catch
			{
				methodResult.Message = "FTP Directory Creation failed for " + folderName + ".";
				methodResult.Success = false;
			}
			return methodResult;
		}

		public YellowstonePathology.Business.Rules.MethodResult CreatePsftpScript(string folderName, Document.CaseDocumentCollection caseDocumentCollection)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = new Rules.MethodResult();
			using (StreamWriter streamWriter = new StreamWriter(this.m_BatchFileName))
			{
				streamWriter.WriteLine("mkdir " + folderName);
				streamWriter.WriteLine("cd " + folderName);
				foreach (Document.CaseDocument caseDocument in caseDocumentCollection)
				{
					streamWriter.WriteLine("put " + caseDocument.FullFileName + " " + caseDocument.FileName);
				}
				streamWriter.WriteLine("quit");
			}
			return methodResult;
		}

		public YellowstonePathology.Business.Rules.MethodResult ExecutePsftpScript(string host, string userName, string password)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = new Rules.MethodResult();
			string arguments = host + " -l " + userName + " -pw " + password + " -b \"" + this.m_BatchFileName + "\" -batch";
			System.Diagnostics.ProcessStartInfo processStartInfo = new System.Diagnostics.ProcessStartInfo(this.m_FileToExecute, arguments);
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.UseShellExecute = false;
			processStartInfo.CreateNoWindow = true;

			System.Diagnostics.Process process = new System.Diagnostics.Process();
			process.StartInfo = processStartInfo;
			process.Start();
			string result = process.StandardOutput.ReadToEnd();
			methodResult.Message = result;
			return methodResult;
		}

		public void CreateDirectoryScript(string folderName, string cdPath)
		{
			using (StreamWriter streamWriter = new StreamWriter(this.m_BatchFileName))
			{
				if (!string.IsNullOrEmpty(cdPath))
				{
					streamWriter.WriteLine("cd " + cdPath);
				}
				streamWriter.WriteLine("mkdir " + folderName);
				streamWriter.WriteLine("quit");
			}
		}

		public void CreateFileScript(Document.CaseDocument caseDocument)
		{
			using (StreamWriter streamWriter = new StreamWriter(this.m_BatchFileName))
			{
				streamWriter.WriteLine("put " + caseDocument.FullFileName + " " + caseDocument.FileName);
				streamWriter.WriteLine("quit");
			}
		}

		public void CreateBillingFileScript(string path, string fileName)
		{
			using (StreamWriter streamWriter = new StreamWriter(this.m_BatchFileName))
			{
				streamWriter.WriteLine("put \"" + path + "\" " + fileName);
				streamWriter.WriteLine("quit");
			}
		}
	}
}
