using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace maintenance.Unit
{
    public class DocService
    {
        private static string UploadFolderDir;

        public string GetUploadFolder()
        {
          
            return UploadFolderDir;
        }
        public Guid SaveFile(Stream file, string fileName)
        {

            Guid fileId = Guid.NewGuid();
            string filePath = GetUploadFolder();
            using (FileStream fs = System.IO.File.Create(GetIDFileName(fileId)))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
            string txtName = GetIDNoteName(fileId);
            System.IO.File.WriteAllText(Path.Combine(filePath, txtName), fileName);
            return fileId;

        }

        private string GetIDFileName(Guid fileID)
        {
            return Path.Combine(GetUploadFolder(), string.Format("{0}.bak", fileID));
        }
        private string GetIDNoteName(Guid fileID)
        {
            return Path.Combine(GetUploadFolder(), string.Format("{0}.txt", fileID));
        }
        public void DeleFileByID(Guid fileID)
        {
            string idfileName = GetIDFileName(fileID);
            if (System.IO.File.Exists(idfileName))
            {
                System.IO.File.Delete(idfileName);
            }
            string notefileName = GetIDNoteName(fileID);
            if (System.IO.File.Exists(notefileName))
            {
                System.IO.File.Delete(notefileName);
            }
        }
        public void DeleFileByIDList(List<Guid> fileIDs)
        {
            if (fileIDs == null) throw new ArgumentNullException("fileIDs");
            foreach (var item in fileIDs)
            {
                DeleFileByID(item);
            }
        }
        public Stream ReadCurFileByID(Guid fileID, out string fileName)
        {

            string idfileName = GetIDFileName(fileID);
            if (File.Exists(idfileName))
            {
                fileName = GetOriginalFileName(fileID);
                return File.OpenRead(idfileName);

            }
            fileName = null;
            return null;
        }

        public bool Exist(Guid fileID)
        {
            string idfileName = GetIDFileName(fileID);
            return File.Exists(idfileName);
        }
        public byte[] GetDocByte(Guid fileID, out string fileName)
        {
            fileName = GetOriginalFileName(fileID);
            string idfileName = GetIDFileName(fileID);
            return File.ReadAllBytes(idfileName);
        }

        private string GetOriginalFileName(Guid fileID)
        {
            string notefileName = GetIDNoteName(fileID);
            return File.ReadAllText(notefileName);
        }


      
        public Stream ReadCurFileByIDList(List<string> fileIDs)
        {
        //    if (fileIDs == null) throw new ArgumentNullException("fileIDs");
        //    var tempFilePath = GetTempFilePath();
        //    string tempfileName = tempFilePath + ".zip";

        //    if (Directory.Exists(tempFilePath))
        //    {
        //        Directory.Delete(tempFilePath, true);
        //    }
        //    Directory.CreateDirectory(tempFilePath);
        //    if (File.Exists(tempfileName))
        //    {
        //        File.Delete(tempfileName);
        //    }
        //    List<string> fileNames = new List<string>();
        //    foreach (var item in fileIDs)
        //    {
        //        string ItemIdName = GetIDFileName(item);
        //        if (File.Exists(ItemIdName))
        //        {
        //            File.Copy(ItemIdName, GetFileName(tempFilePath, item, fileNames), true);
        //        }
        //    }

        //    var memory = new MemoryStream();
        //    ICSharpCode.SharpZipLib.Zip.FastZip zip = new ICSharpCode.SharpZipLib.Zip.FastZip();

        //    zip.CreateZip(tempfileName, tempFilePath, true, "", "");
        //    return File.OpenRead(tempfileName);
         return null;

        }    
       

        
    }

}
