
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Reisinger_Intelliface_1_0.FaceRecognition;
using Reisinger_Intelliface_1_0.Model;
using Reisinger_Intelliface_1_0.Model.ViewModels;
using Reisinger_Intelliface_1_0.Storage;
using System.Linq.Expressions;

namespace Reisinger_Intelliface_1_0.Services;

public class EmployeeService
{
    private readonly IRepository<User> _employeeRepository;
    private readonly IRepository<FaceImage> _faceImageRepository;
    private readonly IFaceRecognizer _faceRecognitionService;
    private string scoreText;
    private string nameText;


    public EmployeeService(IRepository<User> employeeRepository, IFaceRecognizer faceRecognitionService)
    {
        _employeeRepository = employeeRepository;
        _faceRecognitionService = faceRecognitionService;
    }


    public async Task<User> CreateUser(UserFrontViewModel frontViewModel)
    {
        User createdUser = new User()
        {
            ID = Guid.NewGuid(),

            First_Name = frontViewModel.First_Name,
            Last_Name = frontViewModel.Last_Name,
            Gender = frontViewModel.Gender,
            DateOfBirth = frontViewModel.DateOfBirth,
            Email = frontViewModel.Email,
            Nationality = frontViewModel.Nationality,
            Notes = frontViewModel.Notes,
            Collections = frontViewModel.Collections,
            IsBulkInsert = frontViewModel.IsBulkInsert,

        };

        await _employeeRepository.AddAsync(createdUser);

        List<FaceImage> images = new List<FaceImage>();

        foreach (string base64Image in frontViewModel.ImagesAsBase64)
        {
            images.Add(new FaceImage(base64Image, createdUser.ID));
        }
        createdUser.Images = images;
        await _employeeRepository.AddOrUpdateAsync(createdUser);

        // prüfen ob die bilder mitgespeichert werden wenn nur der employeerepo aufgerufen wird, wenn nicht dann die bilder
        // noch durch den FaceImagerepo speichern


        // an dieser stelle sind die daten der business objekte gespeichert

        List<string> imagesConvertedBackAsBase64 = createdUser.Images.Select(m => m.ImageData).ToList();
        bool teachResult = await _faceRecognitionService.SeventhTeach(createdUser, imagesConvertedBackAsBase64);

        createdUser.IsTeached = teachResult;


        return createdUser;
    }

    public async Task<User> UpdateEmployee(UserFrontViewModel updatedUser, List<string> base64Images)
    {
        User? retrievedUser = await _employeeRepository.GetByIdAsync(updatedUser.Id);

        retrievedUser.ID = updatedUser.Id;
        retrievedUser.First_Name = updatedUser.First_Name;
        retrievedUser.Last_Name = updatedUser.Last_Name;
        retrievedUser.Email = updatedUser.Email;
        retrievedUser.Gender = updatedUser.Gender;
        retrievedUser.DateOfBirth = updatedUser.DateOfBirth;
        retrievedUser.Nationality = updatedUser.Nationality;
        retrievedUser.Notes = updatedUser.Notes;
        List<FaceImage> images = new List<FaceImage>();
        foreach (string base64Image in updatedUser.ImagesAsBase64)
        {
            images.Add(new FaceImage(base64Image, updatedUser.Id));
        }
        retrievedUser.Images = images;




        bool teachResult = await _faceRecognitionService.SeventhTeach(retrievedUser, base64Images);
        return retrievedUser;
    }


    public async Task<Guid> AddEmployee(User employee)
    {
        await _employeeRepository.AddAsync(employee);
        return employee.ID;
    }
    public async Task<List<User>> GetAllEmployees()
    {
        return (await _employeeRepository.GetAllAsync()).ToList();
    }
    // In EmployeeService.cs




    public async Task<User> GetEmployeeById(Guid employeeNumber)
    {
        System.Diagnostics.Debug.WriteLine($"UploadImagesAsync aufgerufen mit EmployeeNumber: {employeeNumber}");
        return await _employeeRepository.Find(e => e.ID == employeeNumber).FirstOrDefaultAsync();
    }




}