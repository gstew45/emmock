using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Emmock.Application.Messaging
{
	/// <summary>
	///		Interface defining a contract for a MessageAction.
	/// </summary>
	internal interface IMessageAction
	{
		/// <summary>The message action owner.</summary>
		object Owner { get; }

		/// <summary>
		///		Executes the message action.
		/// </summary>
		/// <param name="parameter">The object to use while executing.</param>
		void Execute(object parameter);
	}

	/// <summary>
	///		MessageAction provides a way to execute an Action of a specific type.
	/// </summary>
	/// <typeparam name="T">Type of action to execute.</typeparam>
	internal class MessageAction<T> : IMessageAction
	{
		private readonly Action<T> m_action;

		/// <summary>
		///		Constructor.
		/// </summary>
		/// <param name="owner">The owner of the message action.</param>
		/// <param name="action">The action to be executed.</param>
		public MessageAction(object owner, Action<T> action)
		{
			Owner = owner;
			m_action = action;
		}

		/// <summary>The message action owner.</summary>
		public object Owner { get; }

		/// <summary>
		///		Executes the message action.
		///		Will execute the action if the parameter type matches the MessageAction type.
		/// </summary>
		/// <param name="parameter">The object to use while executing.</param>
		public void Execute(object parameter)
		{
			if (parameter is T typedParam)
				Execute(typedParam);
			else
				throw new ArgumentException($"Parameter type is not of the correct type {typeof(T)}");
		}

		private void Execute(T parameter)
		{
			if (m_action is Action<T>)
				m_action(parameter);
		}
	}

	/// <summary>
	///		Interface defining the contract for a MessengerHub.
	/// </summary>
	public interface IMessengerHub
	{
		/// <summary>
		///		Registers a recipient for a message of a certain type.
		/// </summary>
		/// <typeparam name="TMessage">Type of message to register a recipient for.</typeparam>
		/// <param name="recipient">The recipient of the message.</param>
		/// <param name="action">The action that will be executed whenever a message of the registered type is sent.</param>
		void Register<TMessage>(object recipient, Action<TMessage> action);

		/// <summary>
		///		Unregisters a recipient from a message of a certain type.
		/// </summary>
		/// <typeparam name="TMessage">Type of message to unregister a recipient from.</typeparam>
		/// <param name="recipient">The recipient to be unregistered from the message type.</param>
		void Unregister<TMessage>(object recipient);

		/// <summary>
		///		Sends a message to all registered recipients.
		/// </summary>
		/// <typeparam name="TMessage">Type of message being sent.</typeparam>
		/// <param name="message">The message being sent.</param>
		void Send<TMessage>(TMessage message);
	}

	/// <summary>
	///		The MessengerHub provides a mechanism for objects to communicate through sending messages.
	/// </summary>
	public class MessengerHub : IMessengerHub
	{
		private readonly Dictionary<Type, List<IMessageAction>> m_messageActions = new Dictionary<Type, List<IMessageAction>>();

		/// <summary>
		///		Registers a recipient for a message of a certain type.
		/// </summary>
		/// <typeparam name="TMessage">Type of message to register a recipient for.</typeparam>
		/// <param name="recipient">The recipient of the message.</param>
		/// <param name="action">The action that will be executed whenever a message of the registered type is sent.</param>
		public void Register<TMessage>(object recipient, Action<TMessage> action)
		{
			if (recipient is null)
				throw new ArgumentNullException(nameof(recipient));

			if (action is null)
				throw new ArgumentNullException(nameof(action));

			Type messageType = typeof(TMessage);
			if (GetActionsByKey(messageType) is null)
				m_messageActions[messageType] = new List<IMessageAction>();

			if (GetActionByRecipient(messageType, recipient) is IMessageAction)
				throw new InvalidOperationException("Message of same type and recipient already registered.");

			m_messageActions[messageType].Add(new MessageAction<TMessage>(recipient, action));
		}

		/// <summary>
		///		Unregisters a recipient from a message of a certain type.
		/// </summary>
		/// <typeparam name="TMessage">Type of message to unregister a recipient from.</typeparam>
		/// <param name="recipient">The recipient to be unregistered from the message type.</param>
		public void Unregister<TMessage>(object recipient)
		{
			if (recipient is null)
				throw new ArgumentNullException(nameof(recipient));

			Type messageType = typeof(TMessage);
			if (GetActionByRecipient(messageType, recipient) is IMessageAction actionToRemove)
				GetActionsByKey(messageType).Remove(actionToRemove);
		}

		/// <summary>
		///		Sends a message to all registered recipients.
		/// </summary>
		/// <typeparam name="TMessage">Type of message being sent.</typeparam>
		/// <param name="message">The message being sent.</param>
		public void Send<TMessage>(TMessage message)
		{
			if (GetActionsByKey(typeof(TMessage)) is List<IMessageAction> actions)
				new ReadOnlyCollection<IMessageAction>(actions)
					.ToList()
					.ForEach(ma => ma.Execute(message));
		}

		private List<IMessageAction> GetActionsByKey(Type messageType)
		{
			if (m_messageActions.TryGetValue(messageType, out List<IMessageAction> actions))
				return actions;

			return null;
		}

		private IMessageAction GetActionByRecipient(Type messageType, object recipient)
		{
			if (GetActionsByKey(messageType) is List<IMessageAction> actions)
				return actions.FirstOrDefault(ma => object.ReferenceEquals(ma.Owner, recipient));

			return null;
		}
	}
}