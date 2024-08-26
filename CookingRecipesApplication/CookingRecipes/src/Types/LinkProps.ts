import { CSSProperties } from 'react';

export default interface LinkProps {
  base?: boolean;
  primary?: boolean;
  to?: string;
  className?: string;
  text?: string;
  onClick?: () => void;
  children?: React.ReactNode;
}
