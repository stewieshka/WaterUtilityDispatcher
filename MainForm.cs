using iTextSharp.text.pdf;
using iTextSharp.text;
using WaterUtilityDispatcher.Data;
using WaterUtilityDispatcher.Domain;
using WaterUtilityDispatcher.Domain.BrigadeRoot;
using WaterUtilityDispatcher.Domain.IncidentRoot;
using WaterUtilityDispatcher.Domain.UserMaterialRoot;
using WaterUtilityDispatcher.Domain.WorkerRoot;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace WaterUtilityDispatcher
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            tabControl1.TabPages.Add(new CustomTabPage<Worker>());
            tabControl1.TabPages.Add(new CustomTabPage<Brigade>());
            tabControl1.TabPages.Add(new CustomTabPage<Incident>());
            tabControl1.TabPages.Add(new CustomTabPage<UsedMaterial>());

            tabControl1.Appearance = TabAppearance.FlatButtons; 
            tabControl1.ItemSize = new Size(0, 1); 
            tabControl1.SizeMode = TabSizeMode.Fixed;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }

        private void MakeReport_Click(object sender, EventArgs e)
        {

            using var context = new AppDbContext();

            IQueryable<Incident> incidents = context.Incidents
                .Include(x => x.UsedMaterials);

            var address = LocationTextBox.Text;

            var type = TypeTextBox.Text;

            if (LocationTextBox.Text.Length != 0)
            {
                incidents = incidents.Where(x => x.Address == address);
            }

            if (TypeTextBox.Text.Length != 0)
            {
                incidents = incidents.Where(x => x.Type == type);
            }

            if (DateOtTextBox.Text.Length != 0)
            {
                if (DateTime.TryParse(DateOtTextBox.Text, out var dateOt))
                {
                    dateOt = DateTime.SpecifyKind(dateOt, DateTimeKind.Utc);
                    incidents = incidents.Where(x => x.Date > dateOt);
                }
            }

            if (DateDoTextBox.Text.Length != 0)
            {
                if (DateTime.TryParse(DateDoTextBox.Text, out var dateDo))
                {
                    dateDo = DateTime.SpecifyKind(dateDo, DateTimeKind.Utc);
                    incidents = incidents.Where(x => x.Date < dateDo);
                }
            }

            var incidentList = incidents.ToList();

            if (incidentList.Count == 0)
            {
                MessageBox.Show("Не найдено ни одного инцидента по указанным данным");
                return;
            }

            GeneratePdfReport(incidentList, "report1.pdf");
        }

        public static void GeneratePdfReport(List<Incident> incidents, string filePath)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string arialPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");

            BaseFont baseFont = BaseFont.CreateFont(arialPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            iTextSharp.text.Font titleFont = new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font headerFont = new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font cellFont = new iTextSharp.text.Font(baseFont, 10);

            Document document = new Document(PageSize.A4);
            PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
            document.Open();

            document.Add(new Paragraph("Отчет об инциденте", titleFont) { Alignment = Element.ALIGN_CENTER });
            document.Add(new Paragraph("\n"));
            document.Add(new Paragraph($"Всего: {incidents.Count}", cellFont) { Alignment = Element.ALIGN_CENTER });
            document.Add(new Paragraph("\n"));

            foreach (var incident in incidents)
            {
                document.Add(new Paragraph($"Вид: {incident.Type}", headerFont));
                document.Add(new Paragraph($"Локация: {incident.Address}", cellFont));
                document.Add(new Paragraph($"Дата: {incident.Date.ToString("yyyy-MM-dd HH:mm")}", cellFont));
                document.Add(new Paragraph($"Описание: {incident.Description}", cellFont));
                document.Add(new Paragraph($"Приоритет: {incident.Priority}", cellFont));
                document.Add(new Paragraph($"Статус: {incident.Status}", cellFont));
                document.Add(new Paragraph("\nИспользованные материалы:", headerFont));
                document.Add(new Paragraph("\n"));

                PdfPTable materialsTable = new PdfPTable(3);
                materialsTable.WidthPercentage = 100;
                materialsTable.AddCell(new PdfPCell(new Phrase("Название", headerFont)));
                materialsTable.AddCell(new PdfPCell(new Phrase("Кол-во", headerFont)));
                materialsTable.AddCell(new PdfPCell(new Phrase("е.и.", headerFont)));

                foreach (var material in incident.UsedMaterials)
                {
                    materialsTable.AddCell(new PdfPCell(new Phrase(material.Name, cellFont)));
                    materialsTable.AddCell(new PdfPCell(new Phrase(material.Amount.ToString(), cellFont)));
                    materialsTable.AddCell(new PdfPCell(new Phrase(material.Unit, cellFont)));
                }

                document.Add(materialsTable);
                document.Add(new Paragraph("\n"));
            }

            document.Close();
        }

    }
}