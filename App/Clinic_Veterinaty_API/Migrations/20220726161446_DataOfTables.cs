using Microsoft.EntityFrameworkCore.Migrations;

namespace Clinic_Veterinaty_API.Migrations
{
    public partial class DataOfTables : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            //Clients
            mb.Sql(@"INSERT INTO clients(CPF, Name, LastName, Address, Email, Status)
            VALUES (12345678901, 'Samuel Noah', 'Francisco Monteiro',
             'Rua Doutor Marcelo Ribeiro Nogueira ,129 - Vila Ema - São Vicente', 'samuel_monteiro@uol.com.br', 1)");
            mb.Sql(@"INSERT INTO clients(CPF, Name, LastName, Address, Email, Status)
            VALUES (12345678902, 'Cláudio Márcio ', 'Oliveira', 
            'Praça Nossa Senhora das Graças,705 - Vila Valença - São Vicente', 'claudio.marcio.oliveira@prudential.com',
             1)"); 
            mb.Sql(@"INSERT INTO clients(CPF, Name, LastName, Address, Email, Status)
            VALUES (87472656801, 'Kauê Emanuel', 'Benedito Gonçalves',
             'Praça Imigração Japonesa, 866, Vila Nossa Senhora de Fátima - São Vicente',
              'kaue-goncalves71@archosolutions.com.br', 1)"); 
            //Vets
            mb.Sql(@"INSERT INTO vets(CRMV, Name, LastName, Address, Email, Status)
            VALUES(1, 'Serion Finhad', 'Matidir', 'Quadra QR 310 Conjunto M, 325,Santa Maria - Brasília',
             'serionfinhad@hotmail.com', 1)");
            mb.Sql(@"INSERT INTO vets(CRMV, Name, LastName, Address, Email, Status)
            VALUES(2, 'Gilcael Gilpo', 'Rayiel Morreis', 
            'Condomínio Residencial Salomão Elias Quadra 3 - 601 - Recanto das Emas - Brasília',
             'gilcaelgilpo@prudential.com', 1)");
            mb.Sql(@"INSERT INTO vets(CRMV, Name, LastName, Address, Email, Status)
            VALUES(3, 'Mornariel Tinuvoi', 'Ceil Edhelil', 'Quadra SQS 109 Bloco D - 843 - Asa Sul - Brasília', 
            'mornarieltinuvoi@yahoo.com', 1)");
            //Dogs
            mb.Sql(@"INSERT INTO dogs(Id, Name, DogWeight, DogBreed, DogHeight, BirthDate, Status, ClientsCPF)
            VALUES(1, 'Cawacau', 10.5, 'Fiokai', 40, '2020-07-21 17:49:30.027000', 1, 12345678901)");
            mb.Sql(@"INSERT INTO dogs(Id, Name, DogWeight, DogBreed, DogHeight, BirthDate, Status, ClientsCPF)
            VALUES(2, 'Xuaclace', 10.9, 'Dodile', 50.5, '2020-07-21 17:49:30.027000', 1, 12345678901)");
            mb.Sql(@"INSERT INTO dogs(Id, Name, DogWeight, DogBreed, DogHeight, BirthDate, Status, ClientsCPF)
            VALUES(3, 'Ricourt Tair', 30.1, 'Dog Aleman', 30, '2020-07-21 17:49:30.027000', 1, 12345678902)");
            mb.Sql(@"INSERT INTO dogs(Id, Name, DogWeight, DogBreed, DogHeight, BirthDate, Status, ClientsCPF)
            VALUES(4, 'Saheoa Deuoa', 15.5, 'Pincher', 30, '2022-07-21 16:51:45.771000', 1, 87472656801)");
            //VetCare
             mb.Sql(@"INSERT INTO vetcare(Id, ClientsCPF, VetsCRMV, DogsId, Hour, Weight, Age, LastDiagnosis, Coments)
             VALUES(1,12345678901, 1, 2, '2022-07-21 12:00:21.527055', 20.3, 2, 'Saude ok', 'Saude ok')");
             mb.Sql(@"INSERT INTO vetcare(Id, ClientsCPF, VetsCRMV, DogsId, Hour, Weight, Age, LastDiagnosis, Coments)
             VALUES(2, 12345678901, 2, 1, '2022-07-21 14:21:24.042253', 20, 2, 'Saude ok', 'Saude ok')");
             mb.Sql(@"INSERT INTO vetcare(Id, ClientsCPF, VetsCRMV, DogsId, Hour, Weight, Age, LastDiagnosis, Coments)
             VALUES(3, 12345678902, 3, 3, '2022-07-21 15:03:29.626581', 30.1, 2, 'Saude ok', 'Saude ok')");
            //System users
            mb.Sql(@"INSERT INTO users(Identification, Email, Password, JobRole)
            VALUES(1, 'SERIONFINHAD@HOTMAIL.COM', 'c3RyaW5n', 'vet')");
            mb.Sql(@"INSERT INTO users(Identification, Email, Password, JobRole)
            VALUES(2, 'GILCAELGILPO@PRUDENTIAL.COM', 'c3RyaW5n', 'vet')");
            mb.Sql(@"INSERT INTO users(Identification, Email, Password, JobRole)
            VALUES(3, 'MORNARIELTINUVOI@YAHOO.COM', 'c3RyaW5n', 'vet')");
            mb.Sql(@"INSERT INTO users(Identification, Email, Password, JobRole)
            VALUES(12345678901, 'SAMUEL_MONTEIRO@UOL.COM.BR', 'c3RyaW5n', 'client')");
            mb.Sql(@"INSERT INTO users(Identification, Email, Password, JobRole)
            VALUES(12345678902, 'CLAUDIO.MARCIO.OLIVEIRA@PRUDENTIAL.COM', 'c3RyaW5n', 'client')");
            mb.Sql(@"INSERT INTO users(Identification, Email, Password, JobRole)
            VALUES(87472656801, 'KAUE-GONCALVES71@ARCHOSOLUTIONS.COM.BR', 'c3RyaW5n', 'client')");

        }
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM  vetcare");
            mb.Sql("DELETE FROM  dogs");
            mb.Sql("DELETE FROM  clients");
            mb.Sql("DELETE FROM  vets");
            mb.Sql("DELETE FROM  users");
        }
    }
}
