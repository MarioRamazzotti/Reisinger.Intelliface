using Reisinger_Intelliface_1_0.Model;

namespace Reisinger_Intelliface_1_0.FaceRecognition;

public interface IFaceRecognizer
{

    /// <summary>
    /// TODO: Replace the FaceImage by an image stream 
    /// </summary>
    /// <param name="recordedImages"></param>
    /// <returns></returns>
    Task<RecognizeResult> SeventhRecognize(List<FaceImage> recordedImages);

    /// <summary>
    /// Use this method to teach the api 
    /// </summary>
    /// <param name="employee"></param>
    /// <param name="teachData">Contains a list of base64 formated images</param>
    /// <returns></returns>
    Task<bool> SeventhTeach(User employee, List<string?> teachData);


}