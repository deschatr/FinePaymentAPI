# FinePaymentAPI

Back-end API of the application ot process fine payments.

Creation instructions:<br>
dotnet new webapi<br>
dotnet new gitignore

# moved to sql server
dotnet add package Microsoft.EntityFrameworkCore<br>
dotnet add package Microsoft.EntityFrameworkCore.SqlServer<br>
!! The sql database has been deleted, and the connection details are not valid anymore (this would not be shared publicly otherwise!)

# for migration purposes
dotnet add package Microsoft.EntityFrameworkCore.Design

# also needed for migration
dotnet tool install --global dotnet-ef

# to accept dev certificated
dotnet dev-certs https --trust

API endpoints:<br>
/api/finepayments [GET] returns List<FinePayment> or NotFound, [POST] accepts FinePayment<br>
/api/finepayments/{id} [GET] returns FinePayment or NotFound, [PUT] accepts FinePayment<br>
/api/finepayments/search [GET] parameters: caseref, onlineaccountref, returns FinePayment or NotFound<br>

FinePayment {<br>
    long Id,<br>
    string? CaseReference,<br>
    string? OnlineAccountReference,<br>
    float Amount,<br>
    DateTime PaymentDueDate,<br>
    bool PaymentCompleted,<br>
    DateTime? PaymentDate,<br>
    string? PaymentReference<br>
}