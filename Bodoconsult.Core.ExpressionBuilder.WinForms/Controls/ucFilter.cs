using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Bodoconsult.Core.ExpressionBuilder.Common;
using Bodoconsult.Core.ExpressionBuilder.Helpers;
using Bodoconsult.Core.ExpressionBuilder.Interfaces;
using Bodoconsult.Core.ExpressionBuilder.Tools;
using Bodoconsult.Core.ExpressionBuilder.WinForms.Models;

namespace Bodoconsult.Core.ExpressionBuilder.WinForms.Controls
{
    // Based on ExpressionBuilder by David Belmont
    // See: https://github.com/dbelmont/ExpressionBuilder/tree/master/ExpressionBuilder
    // Copyright 2017 David Belmont
    // Licensed under the Apache License, Version 2.0 (the "License");
    // you may not use this file except in compliance with the License.
    // You may obtain a copy of the License at
    // http://www.apache.org/licenses/LICENSE-2.0
    // Unless required by applicable law or agreed to in writing, software
    // distributed under the License is distributed on an "AS IS" BASIS,
    // WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    // See the License for the specific language governing permissions and
    // limitations under the License.

    /// <summary>
    /// Description of UcFilter.
    /// </summary>
    public partial class UcFilter : UserControl
    {
        string _typeName = typeof(Person).FullName;
        IExpressionPropertyCollection _properties;

        [Category("Data")]
        public string TypeName
        {
            get => _typeName ?? typeof(Person).FullName;
            set => _typeName = value;
        }

        public string PropertyId => (cbProperties.SelectedItem as ExpressionProperty).Id;

        public Operation Operation => (Operation)Enum.Parse(typeof(Operation), (cbOperations.SelectedItem as dynamic).Id);

        public object Value
        {
            get
            {
                var numberOfValues = new OperationHelper().NumberOfValuesAcceptable(Operation);
                return numberOfValues > 0 ? GetValue("ctrlValue") : null;
            }
        }

        public object Value2
        {
            get
            {
                var numberOfValues = new OperationHelper().NumberOfValuesAcceptable(Operation);
                return numberOfValues == 2 ? GetValue("ctrlValue2") : null;
            }
        }

        private object GetValue(string ctrlName)
        {
            var ctrl = Controls[ctrlName];

            if (ctrl != null)
            {
                var property = _properties[PropertyId];
                var type = Nullable.GetUnderlyingType(property.Info.PropertyType) ?? property.Info.PropertyType;
                if (type == typeof(string)) return ctrl.Text;
                if (type == typeof(DateTime)) return ((DateTimePicker) ctrl).Value;
                if (type == typeof(int)) return Convert.ToInt32(((NumericUpDown) ctrl).Value);
                if (type == typeof(bool)) return bool.Parse(ctrl.Text);
                if (type.IsEnum) return Enum.ToObject(property.Info.PropertyType, ((DomainUpDown) ctrl).SelectedItem);
            }

            return null;
        }

        public override bool ValidateChildren()
        {
            var isValid = base.ValidateChildren();

            if (string.IsNullOrWhiteSpace(cbOperations.Text))
            {
                MessageBox.Show("Please, select an operation");
                isValid = false;
            }

            return isValid;
        }

        public FilterStatementConnector Conector => (FilterStatementConnector)Enum.Parse(typeof(FilterStatementConnector), cbConector.Text);

        public UcFilter()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
        }

        public event EventHandler OnAdd;
        public event EventHandler OnRemove;

        void UcFilterLoad(object sender, EventArgs e)
        {
            LoadProperties();
            LoadOperations();
            cbProperties.SelectedIndex = 0;
            cbConector.SelectedIndex = 0;
        }

        public void LoadProperties()
        {
            var type = Type.GetType(TypeName, true);
            LoadProperties(type);
            cbProperties.ValueMember = "Id";
            cbProperties.DisplayMember = "Name";
            cbProperties.DataSource = _properties.ToList();
            cbProperties.SelectedIndexChanged += cbProperties_SelectedIndexChanged;
        }

        private void LoadOperations()
        {
            LoadValueControls();

            var type = _properties[PropertyId].Info.PropertyType;
            var supportedOperations = OperationHelper.GetSupportedOperationsDisplayItemsLocalized(type)
                                        //new OperationHelper()
                                        //                        .SupportedOperations(type)
                                        //                        .Select(o => new {
                                        //                            Id = o.ToString(),
                                        //                            Name = o.GetDescription(global::ExpressionBuilder.WinForms.Resources.Operations.ResourceManager)
                                        //                        })
                                        .ToArray();

            cbOperations.ValueMember = "Id";
            cbOperations.DisplayMember = "Name";
            cbOperations.DataSource = supportedOperations;
        }

        void cbProperties_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadOperations();
        }

        void LoadValueControls()
        {
            Controls.RemoveByKey("ctrlValue");
            Controls.RemoveByKey("ctrlValue2");

            var ctrl = CreateNewControl();
            ctrl.Name = "ctrlValue";
            ctrl.Size = new Size(300, 20);
            ctrl.Location = new Point(422, 6);
            Controls.Add(ctrl);

            var ctrl2 = CreateNewControl();
            ctrl2.Name = "ctrlValue2";
            ctrl2.Size = new Size(300, 20);
            ctrl2.Location = new Point(727, 6);
            Controls.Add(ctrl2);
        }

        public Control CreateNewControl()
        {
            var info = _properties[PropertyId].Info;
            var underlyingNullableType = Nullable.GetUnderlyingType(info.PropertyType);
            var type = underlyingNullableType ?? info.PropertyType;
            Control ctrl = null;
            if (type.IsEnum || type == typeof(bool))
            {
                ctrl = new DomainUpDown();
                if (type == typeof(bool))
                {
                    (ctrl as DomainUpDown).Items.AddRange(new[] { true, false });
                }
                else
                {
                    (ctrl as DomainUpDown).Items.AddRange(Enum.GetValues(type));
                }
                (ctrl as DomainUpDown).SelectedItem = (ctrl as DomainUpDown).Items[0];
                (ctrl as DomainUpDown).ReadOnly = true;

            }

            if (type == typeof(string))
            {
                ctrl = new TextBox();
            }

            if (type == typeof(DateTime))
            {
                ctrl = new DateTimePicker();
            }

            if (new[] { typeof(int), typeof(double), typeof(float), typeof(decimal) }.Contains(type))
            {
                ctrl = new NumericUpDown();
                (ctrl as NumericUpDown).Value = 0;
            }

            if (ctrl == null) throw new Exception("Type not supported");

            return ctrl;
        }

        public IExpressionPropertyCollection LoadProperties(Type type)
        {
            _properties = ExpressionPropertiesHelper.LoadPropertyCollection(type);
            _properties.LoadProperties(Resources.Person.ResourceManager);
            return _properties;

            //return _properties = new ExpressionPropertyCollection(type, global::ExpressionBuilder.WinForms.Resources.Person.ResourceManager);

        }

        void BtnAddClick(object sender, EventArgs e)
        {
            OnAdd(sender, e);
        }

        void BtnRemoveClick(object sender, EventArgs e)
        {
            OnRemove(sender, e);
        }

        private void cbOperations_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetControlVisibility("ctrlValue", true);
            SetControlVisibility("ctrlValue2", true);

            var numberOfValues = new OperationHelper().NumberOfValuesAcceptable(Operation);
            switch (numberOfValues)
            {
                case 0:
                    SetControlVisibility("ctrlValue", false);
                    SetControlVisibility("ctrlValue2", false);
                    break;
                case 1:
                    SetControlVisibility("ctrlValue2", false);
                    break;
            }
        }

        private void SetControlVisibility(string controlName, bool visible)
        {
            var ctrl = Controls.Find(controlName, false).FirstOrDefault();

            if (ctrl != null)
            {
                ctrl.Visible = visible;
            }
        }
    }
}