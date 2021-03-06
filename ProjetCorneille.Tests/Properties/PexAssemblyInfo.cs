// <copyright file="PexAssemblyInfo.cs">Copyright ©  2018</copyright>
using Microsoft.Pex.Framework.Coverage;
using Microsoft.Pex.Framework.Creatable;
using Microsoft.Pex.Framework.Instrumentation;
using Microsoft.Pex.Framework.Settings;
using Microsoft.Pex.Framework.Validation;

// Microsoft.Pex.Framework.Settings
[assembly: PexAssemblySettings(TestFramework = "VisualStudioUnitTest")]

// Microsoft.Pex.Framework.Instrumentation
[assembly: PexAssemblyUnderTest("ProjetCorneille")]
[assembly: PexInstrumentAssembly("System.Drawing")]
[assembly: PexInstrumentAssembly("AForge.Video.DirectShow")]
[assembly: PexInstrumentAssembly("OpenCvSharp")]
[assembly: PexInstrumentAssembly("System.Core")]
[assembly: PexInstrumentAssembly("AForge.Video")]
[assembly: PexInstrumentAssembly("AForge.Controls")]
[assembly: PexInstrumentAssembly("PresentationCore")]
[assembly: PexInstrumentAssembly("PresentationFramework")]
[assembly: PexInstrumentAssembly("System.Xaml")]
[assembly: PexInstrumentAssembly("Accord.Video.FFMPEG")]
[assembly: PexInstrumentAssembly("AForge.Vision")]

// Microsoft.Pex.Framework.Creatable
[assembly: PexCreatableFactoryForDelegates]

// Microsoft.Pex.Framework.Validation
[assembly: PexAllowedContractRequiresFailureAtTypeUnderTestSurface]
[assembly: PexAllowedXmlDocumentedException]

// Microsoft.Pex.Framework.Coverage
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "System.Drawing")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "AForge.Video.DirectShow")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "OpenCvSharp")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "System.Core")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "AForge.Video")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "AForge.Controls")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "PresentationCore")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "PresentationFramework")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "System.Xaml")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "Accord.Video.FFMPEG")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "AForge.Vision")]

