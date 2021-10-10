namespace json2record.tests.sample2.DTOs { 
    public record GlAccounts { 
        public CustomerLedgerAccount customerLedgerAccount { get; init; } 
        public CustomerLedgerSubaccount customerLedgerSubaccount { get; init; } 
        public SalesAccount salesAccount { get; init; } 
        public SalesNonTaxableAccount salesNonTaxableAccount { get; init; } 
        public SalesExportAccount salesExportAccount { get; init; } 
        public SalesSubaccount salesSubaccount { get; init; } 
        public CashDiscountAccount cashDiscountAccount { get; init; } 
        public CashDiscountSubaccount cashDiscountSubaccount { get; init; } 
        public PrepaymentAccount prepaymentAccount { get; init; } 
        public PrepaymentSubaccount prepaymentSubaccount { get; init; } 
    } 
}