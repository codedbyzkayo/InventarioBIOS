using System;
using System.Linq;
using System.Management;

namespace InventarioBIOS.Handler
{

    public class BiosHandler
    {
        public string? consulta;

        public BiosHandler(string? consulta)
        {
            this.consulta = consulta;
        }

        public Bios? Executa()
        {
            string consultaBios = string.IsNullOrWhiteSpace(consulta)
                ? "SELECT * FROM Win32_BIOS"
                : consulta;

            using var biosSearcher = new ManagementObjectSearcher(consultaBios);
            using var biosResults = biosSearcher.Get();
            ManagementBaseObject? biosData = biosResults
                .Cast<ManagementBaseObject>()
                .FirstOrDefault();

            if (biosData == null)
            {
                return null;
            }

            using var enclosureSearcher = new ManagementObjectSearcher(
                "SELECT SerialNumber, SMBIOSAssetTag FROM Win32_SystemEnclosure");
            using var enclosureResults = enclosureSearcher.Get();
            ManagementBaseObject? enclosureData = enclosureResults
                .Cast<ManagementBaseObject>()
                .FirstOrDefault();

            string? serial = PegaValor(biosData, "SerialNumber")
                ?? PegaValor(enclosureData, "SerialNumber");

            if (serial == null)
            {
                using var productSearcher = new ManagementObjectSearcher(
                    "SELECT IdentifyingNumber FROM Win32_ComputerSystemProduct");
                using var productResults = productSearcher.Get();
                ManagementBaseObject? productData = productResults
                    .Cast<ManagementBaseObject>()
                    .FirstOrDefault();
                serial = PegaValor(productData, "IdentifyingNumber");
            }

            return new Bios
            {
                Nome = PegaValor(biosData, "Name"),
                Descricao = PegaValor(biosData, "Description"),
                Versao = PegaValor(biosData, "Version"),
                Build = PegaValor(biosData, "BuildNumber"),
                Fabricante = PegaValor(biosData, "Manufacturer"),
                Serial = serial,
                VersaoSmbios = PegaValor(biosData, "SMBIOSBIOSVersion"),
                SMBIOSAssetTag = PegaValor(enclosureData, "SMBIOSAssetTag")
            };
        }

        private static string? PegaValor(ManagementBaseObject? objeto, string propriedade)
        {
            if (objeto == null)
            {
                return null;
            }

            string? valor = objeto[propriedade]?.ToString()?.Trim();
            if (string.IsNullOrWhiteSpace(valor)
                || valor.Equals("None", StringComparison.OrdinalIgnoreCase)
                || valor.Equals("To be filled by O.E.M.", StringComparison.OrdinalIgnoreCase)
                || valor.Equals("Default string", StringComparison.OrdinalIgnoreCase)
                || valor.Equals("System Serial Number", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return valor;
        }
    }
}

