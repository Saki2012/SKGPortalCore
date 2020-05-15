using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SKGPortalCore.Core.Libary;

namespace SKGPortalCore.Model.SourceData
{
    /// <summary>
    /// 金流匯款檔
    /// </summary>
    public class RemitInfoModel : IImportSource
    {
        #region Public
        /// <summary>
        /// 檔案中的第N行
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 1. 匯款日期(8)
        /// </summary>
        [Description("匯款日期"), MaxLength(8)] public string RemitDate { get => _RemitDate; set => _RemitDate = value.PadLeft(8, '0').ByteSubString(0, 8); }
        /// <summary>
        /// 2. 匯款時間(6)
        /// </summary>
        [Description("交易日期"), MaxLength(6)] public string RemitTime { get => _RemitTime; set => _RemitTime = value.PadLeft(6, '0').ByteSubString(0, 6); }
        /// <summary>
        /// 3. 超商通路(2)
        /// </summary>
        [Description("超商通路"), MaxLength(2)] public string Channel { get => _Channel; set => _Channel = value.PadLeft(2, '0').ByteSubString(0, 2); }
        /// <summary>
        /// 4. 代收項目(3)
        /// </summary>
        [Description("代收項目"), MaxLength(3)] public string CollectionType { get => _CollectionType; set => _CollectionType = value.PadLeft(3, '0').ByteSubString(0, 3); }
        /// <summary>
        /// 5. 交易金額(11)
        /// </summary>
        [Description("交易金額"), MaxLength(11)] public string Amount { get => _Amount; set => _Amount = value.PadLeft(11, '0').ByteSubString(0, 11); }
        /// <summary>
        /// 6. 批號(2)
        /// </summary>
        [Description("批號"), MaxLength(2)] public string BatchNo { get => _BatchNo; set => _BatchNo = value.PadLeft(2, '0').ByteSubString(0, 2); }
        /// <summary>
        /// 7. 保留(96)
        /// </summary>
        [Description("保留"), MaxLength(96)] public string Empty { get => _Empty; set => _Empty = value.PadLeft(96, '0').ByteSubString(0, 96); }
        /// <summary>
        /// 導入批號
        /// </summary>
        [Description("導入批號")] public string ImportBatchNo { get; set; }
        /// <summary>
        /// Source
        /// </summary>
        [Description("Source")]
        public string Source
        {
            get => $"{_RemitDate}{_RemitTime}{_Channel}{_CollectionType}{_Amount}{_BatchNo}{_Empty}";
            set
            {
                _RemitDate = value.ByteSubString(0, 8);
                _RemitTime = value.ByteSubString(8, 6);
                _Channel = value.ByteSubString(14, 2);
                _CollectionType = value.ByteSubString(16, 3);
                _Amount = value.ByteSubString(19, 11);
                _BatchNo = value.ByteSubString(30, 2);
                _Empty = value.ByteSubString(32, 96);
                Src = value;
            }
        }
        /// <summary>
        /// 原Source
        /// Get：原導入資訊流
        /// Set：源自Source Set
        /// </summary>
        public string Src { get; private set; }
        #endregion
        #region Private
        private string _RemitDate;
        private string _RemitTime;
        private string _Channel;
        private string _CollectionType;
        private string _Amount;
        private string _BatchNo;
        private string _Empty;
        #endregion
    }
}
