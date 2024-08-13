import { CSSProperties } from 'react';

export default interface LinkProps {
  base?: boolean;
  primary?: boolean;
  to?: string;
  newStyle?: CSSProperties;
  className?: string;
  linkText?: string;
  onClick?: () => void;
  children?: React.ReactNode;
}
