namespace source01.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Content")]
    public partial class Content
    {
        public long ID { get; set; }

        [StringLength(250)]
        [Display(Name = "Tên nội dung")]
        [Required]
        public string Name { get; set; }

        [StringLength(250)]
        [Display(Name = "Tiêu đề SEO")]
        [Required]
        public string MetaTitle { get; set; }

        [StringLength(500)]
        [Display(Name = "Mô tả nội dung")]
        [Required]
        public string Description { get; set; }

        [StringLength(250)]
        [Display(Name = "Link hình ảnh")]
        [Required]
        public string Image { get; set; }

        [Display(Name = "Loại")]
        [Required]
        public long? CategoryID { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Mô tả chi tiết")]
        [Required]
        public string Detail { get; set; }

        [Display(Name = "Ưu đãi bán chạy [1,2,3]")]
        [Required]
        public int? Warranty { get; set; }

        [Display(Name = "Ngày tạo [yyyy/mm/dd]")]
        [Required]
        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Người tạo")]
        [Required]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [StringLength(250)]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        public string MetaDescriptions { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        [Display(Name = "Ngày SALES")]
        public DateTime? TopHot { get; set; }

        public int? ViewCount { get; set; }

        [StringLength(500)]
        public string Tags { get; set; }

        [StringLength(2)]
        public string Language { get; set; }
    }
}
