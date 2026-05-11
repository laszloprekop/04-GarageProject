#!/usr/bin/env bash
# Records a scripted full-feature demo of GarageProject and renders it to GIF.
# Requires: tmux, asciinema, agg.

set -euo pipefail
cd "$(dirname "$0")"

CAST=demo.cast
GIF=demo.gif
COLS=100
ROWS=30
SESSION="garage-demo-$$"

# Reset persisted state for a deterministic recording
rm -f GarageProject/garage.json

echo "Building..."
dotnet build GarageProject --nologo -v quiet >/dev/null

cleanup() { tmux kill-session -t "$SESSION" 2>/dev/null || true; }
trap cleanup EXIT

echo "Recording $CAST (${COLS}x${ROWS})..."
tmux new-session -d -s "$SESSION" -x "$COLS" -y "$ROWS" \
    "asciinema rec --overwrite --cols $COLS --rows $ROWS \
     -c 'dotnet run --project GarageProject' '$CAST'"

# Short helper — sends keys to the tmux pane (mixes key names and literals)
k() { tmux send-keys -t "$SESSION" "$@"; }

# Human-style typing: one char at a time with a small delay
TYPE_DELAY=0.09
htype() {
    local s="$1"
    for (( i=0; i<${#s}; i++ )); do
        tmux send-keys -t "$SESSION" -l -- "${s:i:1}"
        sleep "$TYPE_DELAY"
    done
}

# Hover before activating a focused button — viewer sees the focus highlight
HOVER=0.9
hover_press() { sleep "$HOVER"; k Space; }

# Pause after pressing an F-key / shortcut so the dialog has time to render
# *and* the previous frame stays on screen long enough to register the press
SHORTCUT_HOLD=0.7

# ─── Boot ──────────────────────────────────────────────────────────────────
sleep 5

# ─── 1. Park a vehicle (F3) ───────────────────────────────────────────────
# Tab order: TypeList → Reg → Color → Wheels → OK → Cancel
sleep $SHORTCUT_HOLD ; k F3 ; sleep 1.4
k Tab            ; sleep 0.3
htype "ZZZ999"   ; sleep 0.3
k Tab            ; sleep 0.3
htype "Pink"     ; sleep 0.3
k Tab            ; sleep 0.3
htype "4"        ; sleep 0.3
k Tab            ; hover_press      ;# focus → OK, dwell, then activate
sleep 2

# ─── 2. Search by registration (F5) ───────────────────────────────────────
sleep $SHORTCUT_HOLD ; k F5 ; sleep 1.4
htype "ABC"      ; sleep 0.3
k Tab            ; hover_press      ;# focus → Search, dwell, then activate
sleep 2.5
k Escape ; sleep 1.5

# ─── 3. Filter by color (F6) ──────────────────────────────────────────────
# Tab order: Type → Color → Wheels → Filter → Cancel
sleep $SHORTCUT_HOLD ; k F6 ; sleep 1.4
k Tab            ; sleep 0.3
htype "Red"      ; sleep 0.3
k Tab Tab        ; hover_press      ;# focus → Filter, dwell, then activate
sleep 2.5
k Escape ; sleep 1.5

# ─── 4. Vehicle Types (Alt+G, then "t" for "Vehicle _Types") ──────────────
sleep $SHORTCUT_HOLD ; k M-g ; sleep 0.8
k t ; sleep 2.5
k Escape ; sleep 1.5

# ─── 5. Save (F8) ─────────────────────────────────────────────────────────
sleep $SHORTCUT_HOLD ; k F8 ; sleep 1.5

# ─── 6. Unpark first row (F4) ─────────────────────────────────────────────
sleep $SHORTCUT_HOLD ; k F4 ; sleep 1.5

# ─── 7. Reset Garage (Ctrl+G) ─────────────────────────────────────────────
sleep $SHORTCUT_HOLD ; k C-g ; sleep 1.5
k BSpace        ; sleep 0.25
k BSpace        ; sleep 0.25
htype "5"       ; sleep 0.3
k Tab           ; hover_press      ;# focus → Create, dwell, then activate
sleep 2

# ─── 8. Load (F7) — restores saved state ──────────────────────────────────
sleep $SHORTCUT_HOLD ; k F7 ; sleep 2.5

# ─── Final hold, then quit ────────────────────────────────────────────────
sleep 2.5
k F10 ; sleep 2

# Give asciinema a moment to flush the cast
sleep 1

echo "Rendering $GIF..."
agg "$CAST" "$GIF"
echo "Done - $CAST and $GIF refreshed."
