﻿using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.DataAccess.Models;

public class Person
{
    public Guid Id { get; set; }
    public Role Role { get; set; }
    public Helpers.Role RoleInfo { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public virtual ICollection<Workday> Workdays { get; set; }
    public virtual ICollection<WorkTask> WorkTasks { get; set; }
    public virtual ICollection<Activity> Activities { get; set; }
    public virtual ICollection<Delivery> Deliveries { get; set; }
}
