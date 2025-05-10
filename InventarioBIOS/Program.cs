using InventarioBIOS.Servicos;
using InventarioBIOS;
using System;

class Program
{
    static void Main()
    {
        try
        {
            Bios bios = BiosAppServico.PegaBios();

            if (bios != null)
            {
                Console.WriteLine("Informações da BIOS:");
                Console.WriteLine($"  Nome: {bios.Nome}");
                Console.WriteLine($"  Descricao: {bios.Descricao}");
                Console.WriteLine($"  Versão: {bios.Versao}");
                Console.WriteLine($"  Build: {bios.Build}");
                Console.WriteLine($"  Fabricante: {bios.Fabricante}");
                Console.WriteLine($"  Serial: {bios.Serial}");
                Console.WriteLine($"  Versao SMBIOS: {bios.VersaoSmbios}");
                Console.WriteLine($"  SMBIOSAssetTag: {bios.SMBIOSAssetTag}");
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