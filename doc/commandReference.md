# Command Reference
This documentation goes over command syntax and how to utilize this in a script using an example.

Note: Scripts are case-insensitive.   

## Command Syntax
`<input> <wait> <time> <action> <arguments>`

- `<input>`: Dictates which input device is used (keyboard or mouse).
- `<wait>`: Optional keyword stating a delay before execution.
- `<time>`: Required option after `wait` argument, defines the delay in milliseconds. Random times are denoted by an `R` prefix and a given time interval (e.g.: `R[500,1000]`).
- `<action>`: Determines the correlation action of the chosen input.
- `<arguments>`: Parameters given to the action.

### Using the Wait Modifier
Because `InputController` explicitly supports static and randomized delayed actions (e.g., `MouseDelayedButtonDown`, `RandomDelayedKeyboardKeyPress`), the optional `wait` parameter can take either a static number or a random interval:
* **Static Delay:** `wait 500` (Pauses for exactly 500ms before executing the action)
* **Randomized Delay:** `wait R[100,500]` (Pauses for a random time between 100ms and 500ms before executing the action)

## Modifier keys
The engine can make use of modifier keys, because these don't correspond to a symbol, the engine uses its own mapping to map a modifier key to a `string`.

| Modifier Key | String |
| :--- | :--- |
| Shift | `shift` |
| Control | `ctrl` |
| Alt | `alt` |
| Meta/Windows/Super/Command | `oskey` |
| AltGr | `altgr` |
| Capslock | `capslock` |
| NumLock | `numlock` |
| ScrollLock | `scrolllock` | 
## List of Commands

### Mouse Commands (Testing)

| Input | Action | Arguments | Description | Example |
| :--- | :--- | :--- | :--- | :--- |
| `mouse` | `move` / `moveto` | `<x> <y>` | Moves the cursor to the absolute screen coordinates. | `mouse move 1920 1080` |
| `mouse` | `moveby` | `<x> <y>` | Moves the cursor relative to its current position. | `mouse moveby 50 -50` |
| `mouse` | `scroll` | `<direction> <clicks>` | Scrolls the mouse wheel in the given direction. | `mouse scroll down 3` |
| `mouse` | `down` / `hold` | `<button>` | Holds down the specified mouse button. | `mouse hold left` |
| `mouse` | `up` / `release` | `<button>` | Releases the specified mouse button. | `mouse release left` |
| `mouse` | `press` / `click` | `<button>` | Clicks (presses and releases) the specified mouse button. | `mouse click right` |

**Mouse Examples with Wait:**
* `mouse wait 500 press left` *(Translates to: `DelayedMouseButtonPress`)*
* `mouse wait R[100,300] down right` *(Translates to: `RandomDelayedMouseButtonDown`)*

### Keyboard Commands

| Input | Action | Arguments | Description | Example |
| :--- | :--- | :--- | :--- | :--- |
| `keyboard` | `down` / `hold` | `<key>` | Holds down the specified key. | `keyboard hold shift` |
| `keyboard` | `up` / `release` | `<key>` | Releases the specified key. | `keyboard release shift` |
| `keyboard` | `press` / `tap` | `<key>` | Presses and releases the specified key. | `keyboard tap enter` |
| `keyboard` | `combo` | `<modifier> <key>` | Holds a modifier, taps a key, and releases the modifier. | `keyboard combo ctrl c` |
| `keyboard` | `type` | `<text>` | Types out a sequence of keys automatically. | `keyboard type hello world` |

### Engine Commands

While most actions are tied to a specific input device, the controller also supports independent execution delays.
The wait command serves as the action in the command syntax.

| Input | Action | Arguments | Description | Example |
| :--- | :--- | :--- | :--- | :--- |
| `engine` | `wait` | `<time>` | Pauses the script for a static amount of time (ms). | `engine wait 1000` |
| `engine` | `wait` | `R[<min>,<max>]` | Pauses the script for a randomized amount of time (ms). | `engine wait R[500,1500]` |

*(Note: `engine` is used here as a placeholder for the `<input>` field to satisfy the strict syntax requirements for standalone thread delays).*

### Control structure
| Input | Action | Arguments | Description | Example | 
| :--- | :--- | :--- | :--- | :--- |  
| `keyboard` | `ifheld` | `<key>` | Starts a block that is executed if a certain key is held (only supports A-Z and modifier keys). | `keyboard ifheld a` |
| `keyboard` | `endif` | `None` | Ends a keyboard ifheld block. | `keyboard endif` |
| `mouse` | `ifheld` | `<button>` | Starts a block that is executed if a certain mouse button is held. | `mouse ifheld left` |
| `mouse` | `endif` | `None` | Ends a mouse ifheld block. | `mouse endif` |
| `engine` | `ifon` | `<toggle key>` | Starts a block that is executed if a toggle key is toggled on. | `engine ifon capslock` |
| `engine` | `ifoff` | `<toggle key>` | Starts a block that is executed if a toggle key is toggled off. | `engine ifoff capslock` |
| `engine` | `endif` | `None` | Ends an engine ifon or ifoff block | `engine endif` |

### Loops
| Input | Action | Arguments | Description | Example |
| :--- | :--- | :--- | :--- | :--- |
| `engine` | `repeat` | `<count>` | Starts a block that repeats <count> amount of times. | `engine repeat 4` |
| `engine` | `endrepeat` | `None` | Ends an engine repeat block. | `engine endrepeat` |

### Variables
Variables must be declared before any executable commands. Once set, you can reference them later using `${name}` or `$name` in command arguments and text.

| Input | Action | Arguments | Description | Example |
| :--- | :--- | :--- | :--- | :--- |
| `engine` | `setvar` | `<var name> <value>` | Sets a variable. | `engine setvar greeting hello world` |
| `engine` | `updatevar` | `<var name> <value>` | Updates the value of an existing variable. | `engine updatevar greeting hello again` |
| `engine` | `deletevar` | `<var name>` | Deletes a variable. | `engine deletevar greeting` |

**Variable examples:**
* `keyboard type ${greeting}`
* `mouse move ${x} ${y}`
* `keyboard type hello $name`

### Functions
Functions act as named, reusable blocks of commands. Like variables, functions must be declared before executable commands begin. Function definitions may be grouped together after variables and before the main script flow.

| Input | Action | Arguments | Description | Example |
| :--- | :--- | :--- | :--- | :--- |
| `engine` | `setfunction` | `<name>` | Starts a named function block. | `engine setfunction greet` |
| `engine` | `endfunction` | `None` | Ends a function definition block. | `engine endfunction` |

#### Calling a function
Once a function is defined, it can be invoked later in the script with a function call.

| Input | Action | Arguments | Description | Example |
| :--- | :--- | :--- | :--- | :--- |
| `engine` | `callfunction` | `<name>` | Executes the specified function block. | `engine callfunction greet` |

**Function example:**
* `engine setfunction greet`
* `    keyboard type Hello from a function`
* `    keyboard wait 200 press enter`
* `engine endfunction`
* `engine callfunction greet`

### Example Script
```text
# Move the mouse and double click with humanized variance
mouse move 500 500
mouse press left
mouse wait R[50,120] press left

# Wait exactly one second, then type text
engine wait 1000
keyboard type hello world

# Execute a delayed copy command
keyboard wait 500 combo ctrl c