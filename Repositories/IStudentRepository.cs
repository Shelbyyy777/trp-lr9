using EventDaysMvc.Models;

namespace EventDaysMvc.Repositories;

public interface IStudentRepository
{
    IReadOnlyList<Student> GetAll();
    Student? GetById(int id);
}
