using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CSVWriter : MonoBehaviour
{
    public InputField inputField;
    private string csvFilePath = "Assets/items.csv"; // Adjust the file path as needed

    public void WriteToCSV()
    {
        string input = inputField.text;

        if (!string.IsNullOrEmpty(input))
        {
            // Split the input by comma and trim any whitespace
            string[] values = input.Split(',');
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Trim();
            }

            // Check if the input contains all three values
            if (values.Length == 3)
            {
                // Append the values to the CSV file
                using (StreamWriter writer = new StreamWriter(csvFilePath, true))
                {
                    writer.WriteLine(string.Join(",", values));
                }

                Debug.Log("Data written to CSV file successfully.");
            }
            else
            {
                Debug.LogError("Input format is incorrect. Please enter item name, item price, and item weight separated by commas.");
            }
        }
        else
        {
            Debug.LogError("Input is empty. Please enter item data.");
        }
    }
}
