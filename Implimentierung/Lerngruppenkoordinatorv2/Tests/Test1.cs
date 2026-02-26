using Microsoft.VisualStudio.TestTools.UnitTesting;
using LerngruppekoordinatorAufgabe2.Model;

namespace LerngruppekoordinatorTes
{
    [TestClass]
    public class UnitTests
    {
        
        [TestMethod]
        public void Benutzer_Name_WirdKorrektGesetzt()
        {
            var benutzer = new Benutzer { Name = "Jonas" };
            Assert.AreEqual("Jonas", benutzer.Name);
        }

        
        [TestMethod]
        public void Lerngruppe_Fach_WirdKorrektGesetzt()
        {
            var gruppe = new Lerngruppe { Fach = "Informatik" };
            Assert.AreEqual("Informatik", gruppe.Fach);
        }

        
        [TestMethod]
        public void Termin_LerngruppenId_WirdKorrektGesetzt()
        {
            var termin = new Termine { LerngruppenId = 5 };
            Assert.AreEqual(5, termin.LerngruppenId);
        }

        
        [TestMethod]
        public void Benutzer_Fachsemester_IstNullWennNichtGesetzt()
        {
            var benutzer = new Benutzer();
            Assert.IsNull(benutzer.Fachsemester);
        }

        
        [TestMethod]
        public void Lerngruppe_Raum_WirdKorrektGesetzt()
        {
            var gruppe = new Lerngruppe { Raum = 101 };
            Assert.AreEqual(101, gruppe.Raum);
        }
    }
}