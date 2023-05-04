# FinePaymentAPI

Back-end API of the application ot process fine payments.

Creation instructions:<br>
dotnet new webapi<br>
dotnet new gitignore

# moved to sql server
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
!! The sql database has been deleted, and the connection details are not valid anymore (this would not be shared publicly otherwise!)

# for migration purposes
dotnet add package Microsoft.EntityFrameworkCore.Design

# also needed for migration
dotnet tool install --global dotnet-ef

# to accept dev certificated
dotnet dev-certs https --trust

API endpoints:
/api/finepayments [GET] returns List<FinePayment> or NotFound, [POST] accepts FinePayment
/api/finepayments/{id} [GET] returns FinePayment or NotFound, [PUT] accepts FinePayment
/api/finepayments/search [GET] parameters: caseref, onlineaccountref, returns FinePayment or NotFound

FinePayment {
    long Id,
    string? CaseReference,
    string? OnlineAccountReference,
    float Amount,
    DateTime PaymentDueDate
    bool PaymentCompleted,
    DateTime? PaymentDate,
    string? PaymentReference
}