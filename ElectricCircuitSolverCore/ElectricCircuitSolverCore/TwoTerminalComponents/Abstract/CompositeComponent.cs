using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCircuitSolverCore.TwoTerminalComponents.Abstract
{
	public abstract class CompositeComponent : TwoTerminalComponent
	{
		#region Attributes
		protected List<TwoTerminalComponent> _components;
		#endregion

		#region Constructor
		protected CompositeComponent(string? label) : base(label)
		{
			_components = new List<TwoTerminalComponent>();
		}
		#endregion

		#region Properties
		public IReadOnlyCollection<TwoTerminalComponent> Components
		{
			get => _components.AsReadOnly();
		}
		#endregion

		#region Methods
		public abstract CompositeComponent Add(TwoTerminalComponent component);
		public abstract bool Remove(TwoTerminalComponent component);
		#endregion
	}
}
