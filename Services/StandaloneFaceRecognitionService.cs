using Reisinger_Intelliface_1_0.FaceRecognition;
using Reisinger_Intelliface_1_0.Model;

namespace Reisinger_Intelliface_1_0.Services;

public class StandaloneFaceRecognitionService : IFaceRecognizer
{
    public Task<RecognizeResult> SeventhRecognize(List<FaceImage> recordedImages)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SeventhTeach(User employee, List<string?> teachData)
    {
        throw new NotImplementedException();
    }


}