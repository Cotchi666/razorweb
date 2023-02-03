using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

// public class Article
// {

//     public int ID { get; set; }
//     [StringLength(255)]
//     [Required(ErrorMessage ="{0} phải nhập ")]
//     [Display(Name = "Tiêu đề")]

//     public string Title { get; set; }

//     [Display(Name = "Ngày đăng")]
//     [DataType(DataType.Date)]
//     public DateTime PublishDate { get; set; }

//     [Display(Name = "Nội dung")]
//     public string Content { set; get; }
//     using System.ComponentModel;
// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

public class Article
{


    [Key]
    public int ID { get; set; }
    // [BindProperty(SupportsGet = true)]

    [StringLength(255, MinimumLength = 5, ErrorMessage = "{0} phải dài từ {2} tới {1} ký tự")]
    [Required(ErrorMessage = "{0} phải nhập")]
    [Column(TypeName = "nvarchar")]
    [DisplayName("Tiêu đề")]
    public string Title { get; set; }

    [DataType(DataType.Date)]
    [Required(ErrorMessage = "{0} phải nhập")]
    [DisplayName("Ngày tạo")]
    public DateTime PublishDate { get; set; }

    [Column(TypeName = "ntext")]
    [DisplayName("Nội dung")]
    public string Content { get; set; }
}
