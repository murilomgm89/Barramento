using System;
using System.Data;
using Microsoft.VisualBasic.FileIO;

namespace OiWeb.CMS.Handle
{
    public class DataTableHandle
    {

        public static DataTable GetDataTabletFromCsvFile(string filePath)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(filePath))
                {
                    csvReader.SetDelimiters(new string[] { ";" });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        if (fieldData != null)
                        {
                            for (int i = 0; i < fieldData.Length; i++)
                            {
                                if (string.IsNullOrWhiteSpace(fieldData[i]))
                                    fieldData[i] = null;

                            }
                            csvData.Rows.Add(fieldData);
                        }

                    }
                }
            }
            catch (Exception)
            {
                //supressed
            }
            return csvData;
        }
    }
}