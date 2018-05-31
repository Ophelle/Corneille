// <copyright file="CreateCameraViewModelTest.cs">Copyright ©  2018</copyright>
using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetCorneille.ViewModel;

namespace ProjetCorneille.ViewModel.Tests
{
    /// <summary>Cette classe contient des tests unitaires paramétrables pour CreateCameraViewModel</summary>
    [PexClass(typeof(CreateCameraViewModel))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class CreateCameraViewModelTest
    {
    }
}
