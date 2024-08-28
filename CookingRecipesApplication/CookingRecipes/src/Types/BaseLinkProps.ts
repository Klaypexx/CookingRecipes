export default interface BaseLinkProps {
  base?: boolean;
  primary?: boolean;
  to?: string;
  className?: string;
  text?: string;
  onClick?: () => void;
  children?: React.ReactNode;
}
