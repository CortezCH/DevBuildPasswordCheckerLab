using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DevBuildPasswordRegistrationLab
{
    public class QA
    {
        [Theory]
        [InlineData(false, "Bigusername")] //one capital, no number
        [InlineData(false, "biig")] //short lowercase
        [InlineData(false, "325135152")] //check for numbers, no letters
        [InlineData(true, "@Bigusernam9")] //full parameter
        [InlineData(true, "@$Bigusernm9")] //pass through more than one symbol
        public void TestPass(bool expected, string valid)
        {
            //arrange

            //act
            bool actual = Program.ValidPassword(valid);
            //assert
            Assert.Equal(expected, actual);
        }
    }
}
