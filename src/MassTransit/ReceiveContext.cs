﻿// Copyright 2007-2014 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit
{
    using System;
    using System.IO;
    using System.Threading;


    public interface ReceiveContext
    {
        /// <summary>
        /// This token is cancelled when the message reception should no longer be processed
        /// </summary>
        CancellationToken CancellationToken { get; }

        /// <summary>
        ///     Returns the message body as a stream that can be deserialized. The stream
        ///     must be disposed by the caller, a reference is not retained
        /// </summary>
        Stream Body { get; }

        // the amount of time elapsed since the message was read from the queue
        TimeSpan ElapsedTime { get; }

        /// <summary>
        ///     The address on which the message was received
        /// </summary>
        Uri InputAddress { get; }

        /// <summary>
        ///     The content type of the message, as determined by the available headers
        /// </summary>
        string ContentType { get; }

        // true if we know this message is being redelivered after a fault
        bool Redelivered { get; }

        /// <summary>
        ///     Headers specific to the transport
        /// </summary>
        Headers Headers { get; }

        /// <summary>
        /// Notify that a message has been consumed from the received context
        /// </summary>
        /// <param name="elapsed"></param>
        /// <param name="messageType"></param>
        /// <param name="consumerType"></param>
        void NotifyConsumed(TimeSpan elapsed, string messageType, string consumerType);

        /// <summary>
        /// Notify that a message consumer faulted
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="consumerType"></param>
        /// <param name="exception"></param>
        void NotifyFaulted(string messageType, string consumerType, Exception exception);
    }
}