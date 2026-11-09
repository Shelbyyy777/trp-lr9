using EventDaysMvc.Models;

namespace EventDaysMvc.Repositories;

public sealed class InMemoryStudentRepository : IStudentRepository
{
    private readonly Student[] _students =
    [
        new Student { Id = 1, Name = "Анна Иванова", Age = 19 },
        new Student { Id = 2, Name = "Иван Петров", Age = 20 },
        new Student { Id = 3, Name = "Мария Смирнова", Age = 18 }
    ];

    public IReadOnlyList<Student> GetAll() => _students.ToArray();
    public Student? GetById(int id) => _students.FirstOrDefault(student => student.Id == id);
}
