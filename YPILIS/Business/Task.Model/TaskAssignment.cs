using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Task.Model
{
    public class TaskAssignment
    {
        public static string Histology = "Histology";
        public static string Molecular = "Molecular";
        public static string Cytology = "Cytology";
        public static string Transcription = "Transcription";
        public static string Flow = "Flow";

        public static List<string> GetTaskAssignmentList()
        {
            List<string> result = new List<string>();                       
            result.Add(Cytology);            
            result.Add(Flow);
            result.Add(Histology);
            result.Add(Molecular);
            result.Add(Transcription);
            return result;
        }
    }
}
