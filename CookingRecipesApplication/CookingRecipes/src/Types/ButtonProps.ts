import { CSSProperties } from 'react';

export default interface ButtonProps {
  primary?: boolean;
  className?: string;
  type?: 'button' | 'reset' | 'submit' | undefined;
  form?: string;
  navigation?: string;
  newStyle?: CSSProperties;
  buttonText?: string;
  onClick?: (event: React.MouseEvent<HTMLButtonElement>) => void;
  children?: React.ReactNode;
}
