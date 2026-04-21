using System;
using System.Collections.Generic;

namespace StudentManagement.Models;

public partial class Student
{
    /// <summary>
    /// Unique identifier for the student.
    /// </summary>
    public int StudentId { get; set; }

    /// <summary>
    /// Full name of the student. This is a required field.
    /// </summary>
    public string StudentName { get; set; } = null!;

    /// <summary>
    /// Email address of the student. Optional field.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Date when the student record was created. Optional field.
    /// </summary>
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// City where the student resides. Optional field.
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Collection of enrollments associated with this student.
    /// Represents the one-to-many relationship with the Enrollment entity.
    /// </summary>
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
