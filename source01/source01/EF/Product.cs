namespace source01.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public long ID { get; set; }

        [StringLength(250)]
        [Display(Name = "Tên sản phẩm")]
        [Required]
        public string Name { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã sản phẩm")]
        [Required]
        public string Code { get; set; }

        [StringLength(250)]
        [Display(Name = "Tiêu đề SEO")]
        [Required]
        public string MetaTitle { get; set; }

        [StringLength(500)]
        [Display(Name = "Mô tả sản phẩm")]
        [Required]
        public string Description { get; set; }

        [StringLength(250)]
        [Display(Name = "Link hình ảnh")]
        [Required]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImages { get; set; }

        [Display(Name = "Giá gốc sản phẩm")]
        [Required]
        public decimal? Price { get; set; }

        [Display(Name = "Giá SALES")]
        [Required]
        public decimal? PromotionPrice { get; set; }


        [Display(Name = "VAT [True/False]")]
        [Required]
        public bool? IncludedVAT { get; set; }

        [Display(Name = "Số lượng sản phẩm")]
        [Required]
        public int Quantity { get; set; }

        [Display(Name = "Loại sản phẩm [1,2,3]")]
        [Required]
        public long? CategoryID { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Mô tả chi tiết")]
        [Required]
        public string Detail { get; set; }

        [Display(Name = "Ưu đãi bán chạy [1,2,3]")]
        [Required]
        public int? Warranty { get; set; }

        [Display(Name = "Ngày tạo")]
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

        public DateTime? TopHot { get; set; }

        public int? ViewCount { get; set; }
    }
}
