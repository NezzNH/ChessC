using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessC.Pieces;

namespace ChessC.DataTypes
{
    class Field
    {
        private bool isOccupied;
        private coordPair location;
        private List<Claim> claims;
        private color fieldColor;
        private Piece pieceOnField;
        public Field(int normedFieldIndex, color fieldColor)
        {
            this.claims = new List<Claim>();
            this.pieceOnField = null;
            location.collumn = normedFieldIndex % 8;
            location.row = normedFieldIndex / 8;
            this.fieldColor = fieldColor;
        }
        public Field(int row, int collumn, color fieldColor) {
            this.pieceOnField = null;
            location.collumn = collumn;
            location.row = row;
            this.fieldColor = fieldColor;
        }
        public Claim[] getClaims() { return this.claims.ToArray(); }
        public void addClaim(Piece inputPiece) {
            Claim newClaim;
            newClaim.claimant = inputPiece;
            newClaim.isRaycastClaim = inputPiece.isRecurrPiece(); //To-DO: if the bottom TODO is to be done, use this method to match Claim struct exactly
        }
        public void removeClaimOf(Piece inputPiece) {
            if (claims == null || claims.Count == 0) return; //TO-DO: fix necronomicon tier code
            for (int i = 0; i < claims.Count; i++) { 
                if (claims[i].claimant == inputPiece) claims.RemoveAt(i);
                return; 
            } //TO-DO: replace with RemoveAt() function as its possible there are internal optimiziations that make it faster than this code
        }
        public void clearClaims()
        {
            this.claims.Clear();
        }
        public void changeOccupancy(bool inputOccupancy) { this.isOccupied = inputOccupancy; }
        public bool returnOccupancy() { return this.isOccupied; }
        public Piece getPiece() { return this.pieceOnField; }
        public void setPiece(Piece pieceOnField) { this.pieceOnField = pieceOnField; }
        public color getColor() { return this.fieldColor; }
    }

    struct Claim
    {
        public Piece claimant;
        //public claimType type; ?? maybe, it depends on whether it would be useful to differentiate between direct claims
        //or moveable fields, or just keeping claims unanymous, in which case we technically don't even need the struct.
        //we could use a static object ClaimHandler to ease the use of claims, which I think is a smart idea

        //also, i have absolutely no idea why i keep using the pronoun "we". its just me on this project
        //maybe im finally going looney after entrusting the c# collector too much :O
        public bool isRaycastClaim;
    }
}
