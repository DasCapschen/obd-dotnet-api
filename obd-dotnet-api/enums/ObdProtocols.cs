/**
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a copy of
 * the License at
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under
 * the License.
 */

/**
 * All OBD protocols.
 *
 */

using System.Collections;
using System.Collections.Generic;

namespace obd_dotnet_api.enums
{
    public class ObdProtocols
    {
          public static readonly ObdProtocols Auto = new ObdProtocols('0'); //Auto select protocol and save
          public static readonly ObdProtocols SaeJ1850Pwm = new ObdProtocols('1'); //41.6 kbaud
          public static readonly ObdProtocols SaeJ1850Vpw = new ObdProtocols('2'); //10.4 kbaud
          public static readonly ObdProtocols Iso91412 = new ObdProtocols('3'); //5 baud init
          public static readonly ObdProtocols Iso142304Kwp = new ObdProtocols('4');//5 baud init
          public static readonly ObdProtocols Iso142304KwpFast = new ObdProtocols('5'); //Fast init
          public static readonly ObdProtocols Iso157654Can = new ObdProtocols('6'); //11 bit ID, 500 kbaud
          public static readonly ObdProtocols Iso157654CanB = new ObdProtocols('7'); //29 bit ID, 500 kbaud
          public static readonly ObdProtocols Iso157654CanC = new ObdProtocols('8'); //11 bit ID, 250 kbaud
          public static readonly ObdProtocols Iso157654CanD = new ObdProtocols('9'); //29 bit ID, 250 kbaud
          public static readonly ObdProtocols SaeJ1939Can = new ObdProtocols('A'); //29 bit ID, 250 kbaud (user adjustable)
          public static readonly ObdProtocols User1Can = new ObdProtocols('B'); //11 bit ID (user adjustable), 125 kbaud (user adjustable)
          public static readonly ObdProtocols User2Can = new ObdProtocols('C'); //11 bit ID (user adjustable), 50 kbaud (user adjustable)


          public static IEnumerable<ObdProtocols> Values
          {
              get
              {
                  yield return Auto;
                  yield return SaeJ1850Pwm;
                  yield return SaeJ1850Vpw;
                  yield return Iso91412;
                  yield return Iso142304Kwp;
                  yield return Iso142304KwpFast;
                  yield return Iso157654Can;
                  yield return Iso157654CanB;
                  yield return Iso157654CanC;
                  yield return Iso157654CanD;
                  yield return SaeJ1939Can;
                  yield return User1Can;
                  yield return User2Can;
              }
          }

          public char Value { get; private set; }

          private ObdProtocols(char value) => (Value) = (value);
    }
}
