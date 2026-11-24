using InventarioBIOS.Servicos;
using InventarioBIOS;
using System;

class Program
{
    static void Main()
    {
        try
        {
            Bios? bios = BiosAppServico.PegaBios();

            if (bios != null)
            {
                Console.WriteLine("Informações da BIOS:");
                Console.WriteLine($"  Nome: {bios.Nome ?? "Não informado via WMI"}");
                Console.WriteLine($"  Descricao: {bios.Descricao ?? "Não informado via WMI"}");
                Console.WriteLine($"  Versão: {bios.Versao ?? "Não informado via WMI"}");
                Console.WriteLine($"  Build: {bios.Build ?? "Não informado via WMI"}");
                Console.WriteLine($"  Fabricante: {bios.Fabricante ?? "Não informado via WMI"}");
                Console.WriteLine($"  Serial: {bios.Serial ?? "Não informado via WMI"}");
                Console.WriteLine($"  Versao SMBIOS: {bios.VersaoSmbios ?? "Não informado via WMI"}");
                Console.WriteLine($"  SMBIOSAssetTag: {bios.SMBIOSAssetTag ?? "Não informado via WMI"}");
            }
            else
            {
                Console.WriteLine("Não foi possível obter informações da BIOS.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocorreu um erro ao executar o programa: {ex.Message}");
        }
    }
}