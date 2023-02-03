using System.ComponentModel.DataAnnotations;

public class Article
{
    public int ID { get; set; }

    [Display(Name="Tiêu đề")]
    public string Title { get; set; }

    [Display(Name="Ngày đăng")]
    [DataType(DataType.Date)]
    public DateTime PublishDate { get; set; }

    [Display(Name="Nội dung")]
    public string Content {set; get;}
}