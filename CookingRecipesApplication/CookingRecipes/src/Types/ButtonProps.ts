import { CSSProperties } from 'react';

export default interface ButtonProps {
  primary?: boolean;
  className?: string;
  type?: 'button' | 'reset' | 'submit' | undefined;
  form?: string;
  navigation?: string;
  newStyle?: CSSProperties;
  buttonText?: string;
  onClick?: () => void;
  children?: React.ReactNode;
}
