using System.Collections.Generic; 
using System; 

namespace json2record.tests.sample2.DTOs { 
    public class Sample2 { 
        public int internalId { get; init; } 
        public string number { get; init; } 
        public string name { get; init; } 
        public string status { get; init; } 
        public MainAddress mainAddress { get; init; } 
        public MainContact mainContact { get; init; } 
        public string accountReference { get; init; } 
        public CustomerClass customerClass { get; init; } 
        public CreditTerms creditTerms { get; init; } 
        public string currencyId { get; init; } 
        public string creditVerification { get; init; } 
        public double creditLimit { get; init; } 
        public int creditDaysPastDue { get; init; } 
        public InvoiceAddress invoiceAddress { get; init; } 
        public InvoiceContact invoiceContact { get; init; } 
        public bool printInvoices { get; init; } 
        public bool acceptAutoInvoices { get; init; } 
        public bool sendInvoicesByEmail { get; init; } 
        public bool printStatements { get; init; } 
        public bool sendStatementsByEmail { get; init; } 
        public bool printMultiCurrencyStatements { get; init; } 
        public string statementType { get; init; } 
        public DeliveryAddress deliveryAddress { get; init; } 
        public DeliveryContact deliveryContact { get; init; } 
        public string vatRegistrationId { get; init; } 
        public string corporateId { get; init; } 
        public string gln { get; init; } 
        public VatZone vatZone { get; init; } 
        public Location location { get; init; } 
        public DateTime lastModifiedDateTime { get; init; } 
        public DateTime createdDateTime { get; init; } 
        public List<DirectDebitLines> directDebitLines { get; init; } 
        public GlAccounts glAccounts { get; init; } 
        public bool invoiceToDefaultLocation { get; init; } 
        public EInvoiceContract eInvoiceContract { get; init; } 
        public string defaultPaymentMethodId { get; init; } 
    }
} 
