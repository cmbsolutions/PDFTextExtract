Imports System
Imports System.Drawing
Imports Microsoft.VisualBasic.FileIO

Module Program
#Disable Warning CA1416 ' Validate platform compatibility
    Sub Main(args As String())
        Try
            Using fs As New IO.FileStream("E:\My Documents\localRepos\PDFTextExtract\TestData\addresses.txt", IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
                Using ts As New TextFieldParser(fs)
                    ts.SetDelimiters({";"})

                    Dim f As New List(Of Font)({
                                               New Font("Arial", 14.0, FontStyle.Regular),
                                               New Font("Calibri", 14.0, FontStyle.Regular),
                                               New Font("Consolas", 14.0, FontStyle.Regular),
                                               New Font("Verdana", 14.0, FontStyle.Regular)
                                               })

                    Dim b As New SolidBrush(Color.Black)

                    While Not ts.EndOfData
                        Dim fields = ts.ReadFields
                        Dim line = Strings.Join(fields, " ")

                        Using bmp As New Bitmap(1200, 30, Imaging.PixelFormat.Format32bppArgb)
                            Using g = Graphics.FromImage(bmp)
                                g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                                g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

                                g.FillRectangle(New SolidBrush(Color.White), 0, 0, 1200, 30)

                                Dim s = g.MeasureString(line, f(Random.Shared.NextInt64(0, f.Count)))

                                g.DrawString(line, f(Random.Shared.NextInt64(0, f.Count)), b, New PointF(5, 3))

                                bmp.Save($"d:\tessdata\{ts.LineNumber:D5}.png", Imaging.ImageFormat.Png)
                                IO.File.WriteAllText($"d:\tessdata\{ts.LineNumber:D5}.gt.txt", line, Text.Encoding.UTF8)
                            End Using
                        End Using
                    End While
                End Using
            End Using

            Console.WriteLine("done")

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub
#Enable Warning CA1416 ' Validate platform compatibility
End Module
