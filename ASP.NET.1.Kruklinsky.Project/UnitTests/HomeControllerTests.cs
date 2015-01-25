using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcUI.Controllers;
using AmbientDbContext;
using Moq;
using BLL.Interface.Abstract;
using MvcUI.Models;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using MvcUI.Infrastructure.Abstract;
using System.Web.Security;

namespace UnitTests
{
    [TestClass]
    public class HomeControllerTests
    {
        private Mock<ISubjectQueryService> subjectQueryService = new Mock<ISubjectQueryService>();
        private Mock<ITestingService> testingService = new Mock<ITestingService>();
        private Mock<ITestQueryService> testQueryService = new Mock<ITestQueryService>();
        private Mock<ITestSessionFactory> testSessionFactory = new Mock<ITestSessionFactory>();

        #region Constructor

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void Constructor_SubjectQueryServiceIsNull_Exception()
        {
            var controller = new HomeController(null, testingService.Object, testQueryService.Object, testSessionFactory.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void Constructor_TestingServiceIsNull_Exception()
        {
            var controller = new HomeController(subjectQueryService.Object, null, testQueryService.Object, testSessionFactory.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void Constructor_TestQueryServiceIsNull_Exception()
        {
            var controller = new HomeController(subjectQueryService.Object, testingService.Object, null, testSessionFactory.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void Constructor_TestSessionFactoryIsNull_Exception()
        {
            var controller = new HomeController(subjectQueryService.Object, testingService.Object, testQueryService.Object, null);
        }

        [TestMethod]
        public void Constructor_Can()
        {
            var controller = new HomeController(subjectQueryService.Object, testingService.Object, testQueryService.Object, testSessionFactory.Object);
        }

        #endregion

        #region Test

        [TestMethod]
        public void Test_InvalidTestId_Redirect()
        {
            int testId = -1;
            Mock<ITestSession> testSession = new Mock<ITestSession>();
            testSession.SetupGet(t => t.IsStarted).Returns(false);
            testSessionFactory.Setup(t => t.GetTestSession()).Returns(testSession.Object);
            var controller = new HomeController(subjectQueryService.Object, testingService.Object, testQueryService.Object, testSessionFactory.Object);

            RedirectToRouteResult result = (RedirectToRouteResult)controller.Test(testId);

            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["controller"], "Home");
        }

        [TestMethod]
        public void Test_Can()
        {
            int testId = 1;
            testQueryService.Setup(t => t.GetTest(testId)).Returns(new BLL.Interface.Entities.Test
            {
                Questions = new List<BLL.Interface.Entities.Question>()
            });
            Mock<ITestSession> testSession = new Mock<ITestSession>();
            testSession.SetupGet(t => t.IsStarted).Returns(false);
            testSessionFactory.Setup(t => t.GetTestSession()).Returns(testSession.Object);
            var controller = new HomeController(subjectQueryService.Object, testingService.Object, testQueryService.Object, testSessionFactory.Object);

            ActionResult result = controller.Test(testId);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        #endregion

        #region StartTest

        [TestMethod]
        public void StartTest_TestSessionIsStarted_Redirect()
        {
            int testId = 1;
            Mock<ITestSession> testSession = new Mock<ITestSession>();
            testSession.SetupGet(t => t.IsStarted).Returns(true);
            testSessionFactory.Setup(t => t.GetTestSession()).Returns(testSession.Object);
            var controller = new HomeController(subjectQueryService.Object, testingService.Object, testQueryService.Object, testSessionFactory.Object);

            RedirectToRouteResult result = (RedirectToRouteResult)controller.StartTest(testId);

            testQueryService.Verify(t => t.GetTest(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        public void StartTest_InvalidTestId_Redirect()
        {
            int testId = -1;
            Mock<ITestSession> testSession = new Mock<ITestSession>();
            testSession.SetupGet(t => t.IsStarted).Returns(false);
            testSessionFactory.Setup(t => t.GetTestSession()).Returns(testSession.Object);
            var controller = new HomeController(subjectQueryService.Object, testingService.Object, testQueryService.Object, testSessionFactory.Object);

            RedirectToRouteResult result = (RedirectToRouteResult)controller.StartTest(testId);

            testQueryService.Verify(t => t.GetTest(testId), Times.Once);
            testingService.Verify(t => t.StartTest(It.IsAny<string>(),It.IsAny<int>(),It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        public void StartTest_Can()
        {
            int testId = 1;
            testQueryService.Setup(t => t.GetTest(testId)).Returns(new BLL.Interface.Entities.Test
            {
                    Questions = new List<BLL.Interface.Entities.Question>()
            });
            Mock<ITestSession> testSession = new Mock<ITestSession>();
            testSession.SetupGet(t => t.IsStarted).Returns(false);
            testSessionFactory.Setup(t => t.GetTestSession()).Returns(testSession.Object);
            var controller = new HomeController(subjectQueryService.Object, testingService.Object, testQueryService.Object, testSessionFactory.Object);
           
            RedirectToRouteResult result = (RedirectToRouteResult)controller.StartTest(testId);

            testingService.Verify(t => t.StartTest(It.IsAny<string>(), testId, It.IsAny<int>()), Times.Once);
        }
        #endregion

        #region Testing

        [TestMethod]
        public void Testing_TestSessionIsNotStarted_Redirect()
        {
            Mock<ITestSession> testSession = new Mock<ITestSession>();
            testSession.SetupGet(t => t.IsStarted).Returns(false);
            testSessionFactory.Setup(t => t.GetTestSession()).Returns(testSession.Object);
            var controller = new HomeController(subjectQueryService.Object, testingService.Object, testQueryService.Object, testSessionFactory.Object);

            RedirectToRouteResult result = (RedirectToRouteResult)controller.Testing();

            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["controller"], "Home");
        }

        [TestMethod]
        public void Testing_TestSessionIsStarted_ViewResult()
        {
            Mock<ITestSession> testSession = new Mock<ITestSession>();
            testSession.SetupGet(t => t.IsStarted).Returns(true);
            testSessionFactory.Setup(t => t.GetTestSession()).Returns(testSession.Object);
            var controller = new HomeController(subjectQueryService.Object, testingService.Object, testQueryService.Object, testSessionFactory.Object);

            ActionResult result = controller.Testing();

            Assert.IsInstanceOfType(result,typeof(ViewResult));
        }

        #endregion

        #region Testing post

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void Testing_Post_AnswersIsNull_Exception()
        {
            var controller = new HomeController(subjectQueryService.Object, testingService.Object, testQueryService.Object, testSessionFactory.Object);

            var result = controller.Testing(null);
        }

        [TestMethod]
        public void Testing_Post_TestSessionIsNotStarted_Redirect()
        {
            Mock<List<Answers>> answers = new Mock<List<Answers>>();
            Mock<ITestSession> testSession = new Mock<ITestSession>();
            testSession.SetupGet(t => t.IsStarted).Returns(false);
            testSessionFactory.Setup(t => t.GetTestSession()).Returns(testSession.Object);
            var controller = new HomeController(subjectQueryService.Object, testingService.Object, testQueryService.Object, testSessionFactory.Object);
            
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Testing(answers.Object);

            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["controller"], "Home");
        }

        [TestMethod]
        public void Testing_Post_TestSessionIsStarted_Redirect()
        {
            Mock<List<Answers>> answers = new Mock<List<Answers>>();
            Mock<ITestSession> testSession = new Mock<ITestSession>();
            testSession.SetupGet(t => t.IsStarted).Returns(true);
            testSessionFactory.Setup(t => t.GetTestSession()).Returns(testSession.Object);
            var controller = new HomeController(subjectQueryService.Object, testingService.Object, testQueryService.Object, testSessionFactory.Object);

            RedirectToRouteResult result = (RedirectToRouteResult)controller.Testing(answers.Object);

            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["controller"], "Result");
        }

        #endregion
    }
}
