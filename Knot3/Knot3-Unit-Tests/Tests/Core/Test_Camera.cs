﻿using System;
using System.Text;
using System.Collections.Generic;

using NUnit.Framework;

namespace Knot3.UnitTests.Tests.Core
{
	/// <summary>
	/// Zusammenfassungsbeschreibung für Test_Camera
	/// </summary>
	[TestFixture]
	public class Test_Camera
	{
		public Test_Camera()
		{
			//
			// TODO: Konstruktorlogik hier hinzufügen
			//
		}

		private TestContext testContextInstance;

		/// <summary>
		///Ruft den Textkontext mit Informationen über
		///den aktuellen Testlauf sowie Funktionalität für diesen auf oder legt diese fest.
		///</summary>
		public TestContext TestContext
		{
			get {
				return testContextInstance;
			}
			set {
				testContextInstance = value;
			}
		}

		#region Zusätzliche Testattribute
		//
		// Sie können beim Schreiben der Tests folgende zusätzliche Attribute verwenden:
		//
		// Verwenden Sie ClassInitialize, um vor Ausführung des ersten Tests in der Klasse Code auszuführen.
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Verwenden Sie ClassCleanup, um nach Ausführung aller Tests in einer Klasse Code auszuführen.
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Mit TestInitialize können Sie vor jedem einzelnen Test Code ausführen.
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Mit TestCleanup können Sie nach jedem einzelnen Test Code ausführen.
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		[Test]
		public void TestMethod1()
		{
			//
			// TODO: Testlogik hier hinzufügen
			//
		}
	}
}
