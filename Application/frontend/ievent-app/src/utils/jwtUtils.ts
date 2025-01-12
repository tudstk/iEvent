// Define the interface for the JWT payload
export interface JwtPayload {
  sub: string;
  jti: string;
  userId: string;
  email: string;
  isAdmin: string;
  isOrganizer: string;
  isUser: string;
  exp: number;
  iss: string;
}

// Utility function to decode and extract JWT payload
export const extractJwtPayload = (token: string): JwtPayload | null => {
  try {
    // Split the JWT into its parts
    const parts = token.split('.');
    if (parts.length !== 3) {
      throw new Error('Invalid JWT format');
    }

    // Decode the payload part (base64)
    const payload = parts[1];
    const decodedPayload = atob(payload);

    // Parse the decoded JSON string into an object
    const parsedPayload: JwtPayload = JSON.parse(decodedPayload);

    // Validate that all required fields exist (optional but recommended)
    if (
      !parsedPayload.sub ||
      !parsedPayload.jti ||
      !parsedPayload.userId ||
      !parsedPayload.email ||
      !parsedPayload.exp ||
      !parsedPayload.iss
    ) {
      throw new Error('JWT payload is missing required fields');
    }

    return parsedPayload;
  } catch (error) {
    console.error('Failed to extract JWT payload:', error);
    return null;
  }
};


