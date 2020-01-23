using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading.Tasks;

namespace DissertationThemes.ServiceApp
{
    [ServiceContract]
    public interface IDissertationThemesService
    {
        [OperationContract]
        byte[] GenerateDocx(int themeId);
        [OperationContract]
        StProgramDto[] GetStudyPrograms();
        [OperationContract]
        ThemeDto[] GetThemes(int year, int stProgramId);
        [OperationContract]
        int[] GetThemeYears();
    }
}
