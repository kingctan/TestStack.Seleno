using System;
using System.Linq.Expressions;
using NSubstitute;
using TestStack.Seleno.PageObjects;
using TestStack.Seleno.PageObjects.Actions;
using TestStack.Seleno.PageObjects.Controls;
using TestStack.Seleno.Tests.TestObjects;

namespace TestStack.Seleno.Tests.PageObjects.Actions.PageWriter
{
    class When_selecting_a_radio_button_in_Radio_Group : PageWriterSpecification
    {
        private IComponentFactory _componentFactory;
        private IRadioButtonGroup _radioButtonGroup;
        private readonly Expression<Func<TestViewModel, ChoiceType>> _radioButtonGroupPropertySelector = x => x.Choice;

        public void Given_a_drop_down_exists()
        {
            _componentFactory = SubstituteFor<IComponentFactory>();
            _radioButtonGroup = SubstituteFor<IRadioButtonGroup>();

            _componentFactory
                .HtmlControlFor<IRadioButtonGroup>(_radioButtonGroupPropertySelector, Arg.Any<int>())
                .Returns(_radioButtonGroup);    
        }
        
        public void When_selecting_a_radio_button()
        {
            SUT.SelectButtonInRadioGroup(_radioButtonGroupPropertySelector, ChoiceType.Another);
        }

        public void Then_the_component_factory_should_retrieve_the_radio_button_group_control()
        {
            _componentFactory
                .Received()
                .HtmlControlFor<IRadioButtonGroup>(_radioButtonGroupPropertySelector, 20);
        }

        public void AndThen_the_radio_button_group_selected_the_element_matching_the_specified_value()
        {
            _radioButtonGroup.SelectElement(ChoiceType.Another);
        }
    }
}