import { Link } from "react-router-dom";
import type { IconLinkCellProps } from "../models/IconLinkCellProps";

const IconLinkCell = (props: IconLinkCellProps) => {
  const { to, icon, className, label, testId = "link-cell" } = props;

  return (
    <Link to={to} className={className} aria-label={label} data-testid={testId}>
      {icon}
    </Link>
  );
};

export default IconLinkCell;
