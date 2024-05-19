# FerryBooking System README

## Projektbeskrivelse
Dette projekt er udviklet som en del af min eksamensopgave på datamatikeruddannelsen i faget C#. Systemet er designet til at administrere booking af biler og gæster på en færge. Projektet indeholder en MVC-applikation, en MAUI-applikation og en API, der alle arbejder sammen for at levere en komplet løsning.

## MVC Applikation
### Oversigt
MVC-applikationen er udviklet ved hjælp af ASP.NET Core MVC og Razor views. Applikationen giver brugerne mulighed for at administrere biler, gæster og færger gennem en brugervenlig webgrænseflade.

### Funktioner
- **CRUD-operationer**: Tilføj, opdater, slet og vis biler, gæster og færger.
- **Validering**: Sikrer at maks antal gæster og biler ikke overskrides.
- **Visninger**: Bruger Razor og HtmlHelpers til dynamiske og responsive visninger.
- **URL Routing**: Effektiv routing til forskellige dele af applikationen.

### Struktur
- **Controllers**
  - `CarsController.cs`
  - `FerriesController.cs`
  - `GuestsController.cs`
  - `HomeController.cs`
- **Views**
  - Biler: `Create.cshtml`, `Delete.cshtml`, `Details.cshtml`, `Edit.cshtml`, `Index.cshtml`
  - Færger: `Create.cshtml`, `Delete.cshtml`, `Details.cshtml`, `Edit.cshtml`, `Index.cshtml`
  - Gæster: `Create.cshtml`, `Delete.cshtml`, `Details.cshtml`, `Edit.cshtml`, `Index.cshtml`
  - Shared: `_Layout.cshtml`, `Error.cshtml`, `_ValidationScriptsPartial.cshtml`
- **ViewModels**
  - `ErrorViewModel.cs`

### Skærmbillede
![MVC UI](FerryBooking\Images\mvc_ui.png)

## MAUI Applikation
### Oversigt
MAUI-applikationen er udviklet til at give en brugervenlig oplevelse for dem, der ønsker at administrere færgebookinger fra deres computere.

### Funktioner
- **CRUD-operationer**: Tilføj, opdater, slet og vis biler, gæster og færger.
- **Validering**: Sikrer at maks antal gæster og biler ikke overskrides.
- **Visninger**: Bruger XAML til at skabe responsive og brugervenlige sider.

### Struktur
- **Pages**
  - Biler: `CarPage.xaml`, `CreateCarPage.xaml`, `EditCarPage.xaml`
  - Færger: `FerryPage.xaml`, `CreateFerryPage.xaml`, `EditFerryPage.xaml`
  - Gæster: `GuestPage.xaml`, `CreateGuestPage.xaml`, `EditGuestPage.xaml`
- **Services**
  - `CarService.cs`
  - `FerryService.cs`
  - `GuestService.cs`
- **Helpers**
  - `ValidatorHelper.cs`
- **Converters**
  - `BoolToGenderConverter.cs`

### Skærmbillede
![MAUI UI](FerryBooking\Images\maui_ui.png)

## FerryBooking API
### Oversigt
API'en er udviklet til at fungere som backend for MAUI-applikationen. Den leverer dataadgang og forretningslogik via HTTP endpoints.

### Struktur
- **Controllers**
  - `CarsController.cs`
  - `FerriesController.cs`
  - `GuestsController.cs`
- **Data**
  - `FerryContext.cs`
- **Models**
  - `Car.cs`
  - `Ferry.cs`
  - `Guest.cs`
- **ViewModels**
  - `CarViewModel.cs`
- **Migrations**
  - `InitialCreate.cs`
  - `DbContextModelSnapshot.cs`

## Installation
1. Klon repositoryet.
2. Åbn løsningen i Visual Studio.
3. Gå til "Tools" > "NuGet Package Manager" > "Package Manager Console".
4. Kør `Update-Database` for at oprette databasen.
5. Start API-projektet, derefter MAUI- og MVC-projekterne.

## Brug
### API
- Base URL: `http://localhost:7163`
- Endpoints:
  - `/api/cars`
  - `/api/ferries`
  - `/api/guests`

### MAUI App
- Naviger mellem siderne for at tilføje, redigere og slette biler, færger og gæster.

### MVC App
- Brug menuen til at navigere mellem biler, færger og gæster. Brug CRUD-funktioner til at administrere data.
